use BookStoreManager

go;
create or alter proc sp_switch_status(@orderID int) as
begin
	declare @currentStatus int
	declare @deliveringMethod nvarchar(50)
	declare @deliverID int

	select @currentStatus = max(StatusID) from CUSTOMER_ORDER_STATUS where OrderID = @orderID
	if(@currentStatus = 2)
		insert into CUSTOMER_ORDER_STATUS VALUES(@orderID, 4, GETDATE())
	else if(@currentStatus = 4)
	begin
		if(@deliveringMethod = 'SHIPPING')
			insert into CUSTOMER_ORDER_STATUS VALUES(@orderID, 6, GETDATE())
		else
		begin
			insert into CUSTOMER_ORDER_STATUS VALUES(@orderID, 5, GETDATE())
		end
	end
	else if(@currentStatus = 5)
	begin
		insert into CUSTOMER_ORDER_STATUS VALUES(@orderID, 7, GETDATE())
	end
end


--SOME DATA USE FOR TEST PROCEDURE
insert into CUSTOMER_ORDER values(GETDATE(), 10000, 'SHIPPING', 'CASH', null, 1, null)
insert into CUSTOMER_ORDER values(GETDATE(), 10000, 'SHIPPING', 'CURRENT BALANCE', null, 1, null)
insert into CUSTOMER_ORDER values(GETDATE(), 10000, 'RECEIVE AT STORE', 'CASH', null, 1, null)
insert into CUSTOMER_ORDER values(GETDATE(), 10000, 'RECEIVE AT STORE', 'CURRENT BALANCE', null, 1, null)

insert into CUSTOMER_ORDER_STATUS values(3, 4, getdate())
insert into CUSTOMER_ORDER_STATUS values(2, 4, getdate())


--trigger auto switch status to success when give a ship confirmination

insert into SHIP_CONFIRMATION values(getdate(), 'bb', 2, 1)

go;
create or alter trigger tr_update_status_when_given_shipconfirmination on SHIP_CONFIRMATION for insert as
begin
	declare @orderID int
	declare @totalAmount int
	select @orderID = OrderID from inserted
	select @totalAmount = CUSTOMER_ORDER_DETAIL.DetailQuantity from CUSTOMER_ORDER_DETAIL, inserted where inserted.OrderID = CUSTOMER_ORDER_DETAIL.OrderID
	insert into CUSTOMER_ORDER_STATUS VALUES(@orderID, 7, GETDATE())
	update STOCK_INVENTORY set InventoryStockOutTotal = InventoryStockOutTotal + @totalAmount
end
