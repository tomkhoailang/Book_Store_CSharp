use BookStoreManager;
--create customer_order_status
go
create procedure SP_CREATE_CUSTOMER_ORDER_STATUS (@OrderID int, @StatusID int)
as
begin
	insert into CUSTOMER_ORDER_STATUS values(@OrderID, @StatusID, GETDATE())
end

