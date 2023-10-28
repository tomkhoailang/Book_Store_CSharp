use BookStoreManager
go
--check promotion

-- check stock when refresh page

GO
create procedure PROC_CHECK_IN_STORAGE_WHEN_REFRESHING(@sessionEditionIDList nvarchar(max), @sessionQuantityList nvarchar(max)) AS
BEGIN
	declare @editionIDTest int = 0;
	declare @detailQuantityTest int = 0;
	declare @outOfStockIDList table(editionID int)

	declare @cursorEditionID  CURSOR;
	DECLARE @cursorQuantity CURSOR;

	set @cursorEditionID = cursor for
	SELECT value FROM STRING_SPLIT(@sessionEditionIDList, ',')

	set @cursorQuantity = cursor for
	SELECT value FROM STRING_SPLIT(@sessionQuantityList, ',')

	open @cursorEditionID
	open @cursorQuantity

	fetch next from @cursorEditionID into @editionIDTest
	fetch next from @cursorQuantity into @detailQuantityTest
	while @@FETCH_STATUS = 0
	begin
		declare @availableStock int = 0;
		select @availableStock =  InventoryAvailableStock from STOCK_INVENTORY where EditionID = @editionIDTest
		if(@detailQuantityTest > @availableStock)
		begin
			print(@editionIDTest)
			print(@detailQuantityTest)
			insert into @outOfStockIDList (editionID) values (@editionIDTest)
		end
		fetch next from @cursorEditionID into @editionIDTest
		fetch next from @cursorQuantity into @detailQuantityTest
	end
	select * from @outOfStockIDList
END


--drop proc PROC_CHECK_IN_STORAGE_WHEN_REFRESHING
--select * from CUSTOMER_ORDER_DETAIL where OrderID = 7
--exec PROC_CHECK_IN_STORAGE_WHEN_REFRESHING '1,2' ,'180, 760'
--select * from STOCK_INVENTORY



---- create attach account to customer

go
create proc SP_Inital_Manager(@AccountID nvarchar(128)) as
begin
	INSERT INTO MANAGER(ManagerName, ManagerPhoneNumber, ManagerAddress, AccountID) VALUES
    ('Admin', null, 'Address', @AccountID)
end
-----------

go
create proc SP_Inital_Customer(@AccountID nvarchar(128)) as
begin
	INSERT INTO CUSTOMER(CustomerName, CustomerPhoneNumber, CustomerAddress, AccountID) VALUES
    ('Customer', null, 'Address', @AccountID)
end


drop proc SP_Inital_Customer




select * from aspnetusers

select * from CUSTOMER

