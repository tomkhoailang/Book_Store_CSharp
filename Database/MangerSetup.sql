use BookStoreManager
--add constraint
ALTER TABLE Person ADD CONSTRAINT FK_Person_AspNetUsers FOREIGN KEY (AccountID) REFERENCES AspNetUsers(Id)
ALTER TABLE MANAGER ADD CONSTRAINT FK_MANAGER_AspNetUsers FOREIGN KEY (AccountID) REFERENCES AspNetUsers(Id)


insert into ORDER_STATUS values ('INITIAL'),
('WAITING'),
('CANCEL BY CUSTOMER'),
('PROCESSING'),
('IS AVAILABLE AT STORE'),
('DELIVERING'),
('SUCCESS'),
('CANCEL BECAUSE OF MANY FAILED DELIVERING')

--set default roles 
insert into AspNetRoles(Id, Name) values
	(1,'Manager'),
	(4,'Customer'),
	(2,'Staff'),
	(3,'Shipper')


--Trigger on Manager
--Have only 1 admin
go
CREATE or alter TRIGGER TR_ONLY_ONE_MANAGER ON MANAGER for insert AS
BEGIN
	IF (SELECT count(*) FROM MANAGER ) = 2
	BEGIN
		raiserror('only 1 admin',16,1)
		ROLLBACK TRAN
	END
END


--Trigger on Person
--Inital wallet if the person is customer
go
CREATE TRIGGER TR_INITIAL_PERSON ON Person for insert AS
BEGIN
	declare @AccountID nvarchar(128) = (select AccountID from inserted)
	declare @CustomerID nvarchar(128) = (select PersonID from inserted)
	if exists(select * from AspNetUserRoles where UserId = @AccountID and RoleId = 4)
	begin
		INSERT INTO WALLET(CustomerID, Balance) VALUES (@CustomerID, 0)
	end
END


-- triger on BOOK_EDITION TO INITIAL STOCK_INVENTORY
GO
CREATE or alter TRIGGER TR_INITIAL_STOCK_INVENTORY ON BOOK_EDITION FOR INSERT AS
BEGIN
	DECLARE @editionID int;
	select @editionID = EditionID from inserted 
	insert into STOCK_INVENTORY (EditionID) values(@editionID)
END


-- TRIGER ON TR_STOCK_INVENTORY TO UPDATE InventoryAvailableStock
GO
CREATE or alter TRIGGER TR_UPDATE_INVENTORY_AVAILABLE_STOCK ON STOCK_INVENTORY FOR UPDATE AS
BEGIN
	IF UPDATE(InventoryStockInTotal) OR UPDATE(InventoryStockOutTotal)
    BEGIN
		update STOCK_INVENTORY set InventoryAvailableStock = i.InventoryStockInTotal - i.InventoryStockOutTotal
		from inserted i where STOCK_INVENTORY.EditionID = i.EditionID
	end
END


-- trigger ON STOCK_RECEIVED_NOTE_DETAIL to update InventoryStockInTotal
go
CREATE or alter TRIGGER TR_UPDATE_INVENTORY_STOCK_IN_TOTAL on STOCK_RECEIVED_NOTE_DETAIL for insert,update,delete as
begin
	--declare @isNewEdition bit;
	declare @editionID int;
	declare @insertedQuantity int = 0;
	declare @deletedQuantity int = 0;

	select @editionID = EditionID from inserted i
	if exists (select * from deleted)
	begin
		select @editionID = EditionID from deleted i
	end

	select @insertedQuantity = NoteDetailQuantity from inserted 
	select @deletedQuantity = NoteDetailQuantity from deleted

	UPDATE STOCK_INVENTORY set InventoryStockInTotal = InventoryStockInTotal + @insertedQuantity - @deletedQuantity
	where STOCK_INVENTORY.EditionID = @editionID

end



go
CREATE or alter TRIGGER TR_CHECK_PROMOTION_DATE ON Promotion for insert,update AS
BEGIN

	if(SELECT PromotionStartDate FROM inserted) >= (SELECT PromotionEndDate FROM inserted)
	begin
		raiserror('Prmotion start date must bed less than promotion end date',16,1)
		rollback tran
	end
	else
	begin
		if not exists(select * from deleted) 
		begin
			if (SELECT PromotionStartDate FROM inserted) < GETDATE()
			begin
				raiserror('When creating, prmotion start date must be not in the past and less than promotion end date',16,1)
			end
		end
		else
		begin
			if(update(PromotionStartDate))
			begin
				raiserror('When editing, promotion start date could not be changed',16,1)
			end
		end
	end
END

----------------------------------------------------------------------------------------


--Store procedure
--Inital Person
go
create or alter proc SP_Inital_Person(@AccountID nvarchar(128), @ManagerID int) as
begin
	INSERT INTO Person(PersonName, PersonAddress, AccountID, ManagerID) VALUES
    ('', '', @AccountID, @ManagerID)
end

--initial manager
go
create proc SP_Inital_Manager(@AccountID nvarchar(128)) as
begin
	INSERT INTO MANAGER(AccountID) VALUES (@AccountID)
end
--select * from MANAGER
--select * from aspnetroles
--select * from aspnetuserroles
--check promotion
go
create  proc Sp_check_valid_promotion(@editionID int) as
begin
	select top 1 p.* from PROMOTION p, BOOK_PROMOTION bp
	where getdate() between p.PromotionStartDate and p.PromotionEndDate and
	p.PromotionID = bp.PromotionID and bp.EditionID = @editionID 
	order by p.PromotionDiscount desc
end


--View
-- view user with role
go
create or alter view V_UserRole as
select anr.Id,anr.Name, anur.UserId, p.PersonID 
from AspNetRoles anr, AspNetUserRoles anur,Person p
where anr.Id = anur.RoleId and anur.UserId = p.AccountID


-- view customer spending
go
create or alter view V_CustomerSpending as
select CustomerID, sum(c.OrderTotalPrice) as TotalSpending 
from CUSTOMER_ORDER_STATUS cs, CUSTOMER_ORDER c
where cs.OrderID = 7 and cs.OrderID = c.OrderID
group by c.CustomerID



----Function
----get the current tierID
go
create or alter function FN_GetTierID(@personID int) returns int as
begin
	return 
	(select top 1 TierID from TIER where TierLevel <= 
	(select v.TotalSpending 
	from Person p, V_CustomerSpending v 
	where p.PersonID = @personID and p.PersonID = v.CustomerID)
	order by TierLevel)
end


----Trigger on Tier
go
create or alter trigger TR_UPDATE_TIER_OF_CUSTOMER on Tier after insert,delete, update as
begin
	declare @currentCustomerID int;
	
	declare currentCursor cursor for select p.PersonID from Person p, V_UserRole v 
	where p.PersonID = v.PersonID and v.Id = 4;

	open currentCursor
	fetch next from currentCursor into @currentCustomerID
	while @@FETCH_STATUS = 0
	begin
		declare @tierID int;
		set @tierID =  dbo.FN_GetTierID(@currentCustomerID)
		if(@tierID != null)
		begin
			update Person set TierID = @tierID where PersonID = @currentCustomerID
		end
		fetch next from currentCursor into @currentCustomerID
	end
	CLOSE currentCursor
	DEALLOCATE currentCursor
end




-- view thong tin nhap xuat cua tung edition

go
create view V_edition_total_stock_quantity_price_in_this_and_previous_month as
SELECT
    sd.EditionID,
    SUM(CASE WHEN MONTH(s.StockReceivedNoteDate) = MONTH(GETDATE()) THEN sd.NoteDetailQuantity ELSE 0 END) AS TongNhapCuaThang,
	SUM(CASE WHEN MONTH(s.StockReceivedNoteDate) = MONTH(DATEADD(MONTH, -1, GETDATE())) THEN sd.NoteDetailQuantity ELSE 0 END) AS TongNhapThangTruoc,
    SUM(CASE WHEN MONTH(s.StockReceivedNoteDate) = MONTH(GETDATE()) THEN (sd.NoteDetailQuantity * sd.NoteDetailPrice) ELSE 0 END) AS TongTienNhapCuaThang,
    SUM(CASE WHEN MONTH(s.StockReceivedNoteDate) = MONTH(DATEADD(MONTH, -1, GETDATE())) THEN (sd.NoteDetailQuantity * sd.NoteDetailPrice) ELSE 0 END) AS TongTienNhapThangTruoc
FROM
    STOCK_RECEIVED_NOTE s
    JOIN STOCK_RECEIVED_NOTE_DETAIL sd ON s.StockReceivedNoteID = sd.StockReceivedNoteID
WHERE
    MONTH(s.StockReceivedNoteDate) IN (MONTH(GETDATE()), MONTH(DATEADD(MONTH, -1, GETDATE())))
	AND YEAR(s.StockReceivedNoteDate) = Year(getdate())
GROUP BY
    sd.EditionID;





-- view doanh thu tung ngay
go
create or alter view V_revenue_of_each_day as
select 
ISNULL(ROW_NUMBER() over(order by  YEAR(c.OrderDate), MONTH(c.OrderDate),  day(c.OrderDate)), 0)  as ID,
YEAR(c.OrderDate) AS Year, MONTH(c.OrderDate) AS Month, Day(c.OrderDate) AS Day, SUM(c.OrderTotalPrice) AS Revenue
from CUSTOMER_ORDER_STATUS cs, CUSTOMER_ORDER c
where cs.StatusID = 7 and cs.OrderID = c.OrderID
group by year(c.OrderDate) , month(c.OrderDate), day(c.OrderDate)


-- view doanh thu tung thang cua thang

go
create or alter view V_revenue_of_each_month as
select ISNULL(ROW_NUMBER() over(order by  Year, Month), 0)  as ID , Year , Month, SUM(Revenue) AS Revenue
from V_revenue_of_each_day
group by Year, Month

go
create or alter view V_revenue_of_each_year as
select ISNULL(ROW_NUMBER() over(order by  Year), 0)  as ID , Year , SUM(Revenue) AS Revenue
from V_revenue_of_each_month
group by Year

go
create or alter view V_edition_buy_count as
select EditionID, count(EditionID) as BuyCount
from CUSTOMER_ORDER_DETAIL
group by editionID

select * from BOOK_EDITION b, V_edition_buy_count v
where b.EditionID = v.EditionID
---

go
create or alter trigger SetPromotionDetial on PROMOTION for insert, update
as
begin
	declare @promoDetail nvarchar(MAX);
	declare @promoID int;
	select @promoDetail = i.PromotionDetails, @promoID = i.PromotionID from inserted i;
	declare @firstKey nvarchar(2) = left(@promoDetail,2);
	declare @lastKey nvarchar(1) = right(@promoDetail,1);
	if @firstKey = 'N''' and @lastKey = ''''
	begin
		declare @detail nvarchar(MAX) = left(@promoDetail,len(@promoDetail)-1);
		update PROMOTION set PromotionDetails = right(@detail,len(@detail) -2) where PromotionID = @promoID
	end
end


go
create or alter trigger SetPromotionDetial on PROMOTION for insert, update
as
begin
	declare @promoDetail nvarchar(MAX);
	declare @promoID int;
	select @promoDetail = i.PromotionDetails, @promoID = i.PromotionID from inserted i;
	declare @firstKey nvarchar(2) = left(@promoDetail,2);
	declare @lastKey nvarchar(1) = right(@promoDetail,1);
	if @firstKey = 'N''' and @lastKey = ''''
	begin
		declare @detail nvarchar(MAX) = left(@promoDetail,len(@promoDetail)-1);
		update PROMOTION set PromotionDetails = right(@detail,len(@detail) -2) where PromotionID = @promoID
	end
end



