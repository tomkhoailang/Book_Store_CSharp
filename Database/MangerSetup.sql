use BookStoreManager
--constraint
ALTER TABLE Person ADD CONSTRAINT FK_Person_AspNetUsers FOREIGN KEY (AccountID) REFERENCES AspNetUsers(Id)
ALTER TABLE MANAGER ADD CONSTRAINT FK_MANAGER_AspNetUsers FOREIGN KEY (AccountID) REFERENCES AspNetUsers(Id)
--set roles 
insert into AspNetRoles(Id, Name) values
	(1,'Manager'),
	(4,'Customer'),
	(2,'Staff'),
	(3,'Shipper')
-- triger to have only 1 admin
go
CREATE or alter TRIGGER TR_ONLY_ONE_MANAGER ON MANAGER for insert AS
BEGIN
	IF (SELECT count(*) FROM MANAGER ) = 2
	BEGIN
		raiserror('only 1 admin',16,1)
		ROLLBACK TRAN
	END
END


go
CREATE or alter TRIGGER TR_INITIAL_MANAGER_ROLE ON MANAGER for insert AS
BEGIN
	DECLARE @AccountID  NVARCHAR(128);
	SELECT @AccountID =  AccountID from INSERTED
	INSERT INTO AspNetUserRoles(UserId,RoleId) VALUES (@AccountID, 1)
END



-- inital user
go
create  proc sp_Inital_Manager(@AccountID nvarchar(128)) as
begin
	INSERT INTO MANAGER(AccountID) VALUES (@AccountID)
end
-----------

--go
--create or alter proc SP_Inital_Customer(@AccountID nvarchar(128)) as
--begin
--	INSERT INTO Person(PersonName, PersonAddress, AccountID) VALUES
--    ('Customer', 'Address', @AccountID)
--end
--trigger to set manager role


--trigger to set customer role. check role
--go
--CREATE TRIGGER TR_INITIAL_CUSTOMER_ROLE ON Person for insert AS
--BEGIN
--	DECLARE @AccountID NVARCHAR(128);
--	SELECT @AccountID =  AccountID from INSERTED
--	INSERT INTO AspNetUserRoles(UserId,RoleId) VALUES (@AccountID, 4)
--END


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

select * from STOCK_INVENTORY
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
--check promotion
go
create  proc Sp_check_valid_promotion(@editionID int) as
begin
	select top 1 p.* from PROMOTION p, BOOK_PROMOTION bp
	where getdate() between p.PromotionStartDate and p.PromotionEndDate and
	p.PromotionID = bp.PromotionID and bp.EditionID = @editionID 
	order by p.PromotionDiscount desc
end



