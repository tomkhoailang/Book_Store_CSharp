
go
CREATE TRIGGER TR_CALCULATE_BALANCE_OF_CUSTOMER_FROM_ORDER ON CUSTOMER_ORDER FOR INSERT, UPDATE, DELETE AS
BEGIN
	declare @customerID int;
	declare @OrderPaymentMethod NVARCHAR(50);
	declare @TotalPrice DECIMAL(12,2);
	declare @OrderStatus NVARCHAR(50);

	select @customerID = i.CustomerID, 
			@OrderPaymentMethod = i.OrderPaymentMethod, 
			@TotalPrice = i.OrderTotalPrice, 
			@OrderStatus = i.OrderStatus from inserted i;

	if @OrderPaymentMethod in ('CURRENT BALANCE') and @OrderStatus in ('INITIAL')
	begin
		declare @oldPrice decimal(12,2) = 0;
		Declare @currentBalance decimal(12,2) = 0;
		select @oldPrice = d.OrderTotalPrice from deleted d;
		select @currentBalance = W.Balance FROM WALLET W WHERE W.CustomerID = @customerID
		update WALLET 
		set Balance = @currentBalance - @TotalPrice + @oldPrice
		where CustomerID = @customerID
	end
END

drop TRIGGER TR_CALCULATE_BALANCE_OF_CUSTOMER_FROM_ORDER
--trigger to check if book is in stock when adding
--note when the user taps on proceed to payment, a order will be created and each book from card will be added to this order
GO
CREATE TRIGGER TR_UPDATE_STOCK_WITH_INSERT ON CUSTOMER_ORDER_DETAIL FOR INSERT AS
BEGIN
	DECLARE @editionID int = 0;
	DECLARE @currentAvailableStock int = 0;
	DECLARE @orderQuantity int = 0;
	DECLARE @orderID int = 0
	
	SELECT @editionID = EditionID,  @orderQuantity = DetailQuantity, @orderID = OrderID from INSERTED
	SELECT @currentAvailableStock = InventoryAvailableStock from STOCK_INVENTORY where EditionID = @editionID
	if(@orderQuantity > @currentAvailableStock)
	begin
		print('The number of quantity is not enough in storage for book id : '+ cast(@editionID as varchar(20)))
		delete CUSTOMER_ORDER_DETAIL where OrderID = @orderID and EditionID = @editionID
	end
	else
	begin
		update STOCK_INVENTORY set InventoryStockOutTotal = InventoryStockOutTotal + @orderQuantity 
		where EditionID = @editionID
	end
END

GO	
create or alter trigger TR_HANDLE_CUSTOMER_ORDER ON CUSTOMER_ORDER for  UPDATE AS
BEGIN
	Declare @customerID int;
	Declare @orderID int;
	DECLARE @orderCurrentTotalPrice decimal(12,2) = 0;

	select @customerID = W.CustomerID from Person p, INSERTED I, WALLET W
	where p.PersonID = I.CustomerID AND p.PersonID = W.CustomerID

	SELECT @orderCurrentTotalPrice = I.OrderTotalPrice FROM INSERTED I
	
	select @orderID = OrderID from inserted

	IF (select count(*) from DELETED  where OrderPaymentMethod = 'CURRENT BALANCE' and OrderStatus = 'INITIAL') >0
	and 
	(SELECT COUNT(*) FROM INSERTED I WHERE I.OrderPaymentMethod = 'CURRENT BALANCE' AND OrderStatus = 'WAITING' ) > 0
	BEGIN
		-- check current balance
		IF(SELECT COUNT(*) FROM WALLET WHERE CustomerID = @customerID AND Balance >= @orderCurrentTotalPrice ) > 0
			UPDATE WALLET SET Balance = Balance - @orderCurrentTotalPrice WHERE CustomerID = @customerID
		begin
			print('Current balance of CustomerID:  ' + CAST(@customerID as varchar(10)) + ' for OrderID: ' + cast(@orderID as varchar(10)) +' is not enough')
			--rollback tran
			delete CUSTOMER_ORDER_DETAIL where OrderID = @orderID
			delete CUSTOMER_ORDER where OrderID = @orderID
		end
	END


	--note: sua cai nay nhe dai ka duy dep zai
	else IF (select  count(*) from DELETED D where OrderPaymentMethod = 'CURRENT BALANCE' and OrderStatus = 'WAITING' ) > 0
	and 
	--note: tach ra if cancel by customer then return value, in the if, check if using current balance, return the balance
	(SELECT COUNT(*) FROM INSERTED I WHERE I.OrderPaymentMethod = 'CURRENT BALANCE' AND I.OrderStatus = 'CANCEL BY CUSTOMER' ) > 0
	begin
		SELECT @orderCurrentTotalPrice = I.OrderTotalPrice FROM INSERTED I
		UPDATE WALLET SET Balance = Balance + @orderCurrentTotalPrice WHERE CustomerID = @customerID
	end
END

-- CUSTOMER_ORDER TABLE

-- trigger to calculate OrderTotalPrice from  DetailCurrentPrice and  DetailQuantity

go
create or alter trigger TR_CALCULATE_TOTAL_PRICE_FROM_ORDER_DETAIL on CUSTOMER_ORDER_DETAIL for insert as
begin
	declare @tierID int ;
	select @tierID = Person.TierID from inserted i, Person , CUSTOMER_ORDER
	where i.OrderID = CUSTOMER_ORDER.OrderID and CUSTOMER_ORDER.CustomerID = Person.PersonID

	declare @tierDiscount decimal(4,2);
	select @tierDiscount = TierDiscount from TIER where TierID = @tierID
	update CUSTOMER_ORDER set OrderTotalPrice =
	OrderTotalPrice + (i.DetailCurrentPrice * i.DetailQuantity) - (i.DetailCurrentPrice * i.DetailQuantity)*@tierDiscount/100
	from inserted i where CUSTOMER_ORDER.OrderID = i.OrderID
end

GO
CREATE or alter TRIGGER TR_UPDATE_INVENTORY_AVAILABLE_STOCK ON STOCK_INVENTORY FOR UPDATE AS
BEGIN
	IF UPDATE(InventoryStockInTotal) OR UPDATE(InventoryStockOutTotal)
    BEGIN
		update STOCK_INVENTORY set InventoryAvailableStock = i.InventoryStockInTotal - i.InventoryStockOutTotal
		from inserted i where STOCK_INVENTORY.EditionID = i.EditionID
	end
END


select * from CUSTOMER_ORDER_DETAIL
select * from CUSTOMER_ORDER
select * from STOCK_INVENTORY