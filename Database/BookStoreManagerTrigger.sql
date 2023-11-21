-- CUSTOMER TABLE
-- trigger to calculate balance from transaction
use BookStoreManager;
GO
CREATE TRIGGER TR_CALCULATE_BALANCE_OF_CUSTOMER_FROM_TRANSACTION_DETAIL ON  TRANSACTION_DETAILS FOR INSERT AS
BEGIN
	Declare @walletID INT;
	select @walletID =  W.WalletID from WALLET  W, INSERTED I
	WHERE W.WalletID = I.WalletID

	Declare @currentBalance decimal(12,2) = 0;
	Declare @insertedTransaction decimal(12,2) = 0;

	select @currentBalance = W.Balance FROM WALLET W WHERE W.WalletID = @walletID
	select @insertedTransaction =  i.TransactionAmount  FROM INSERTED i 

	if exists (select 1 from inserted i where i.TransactionType like ('Deposit'))
	begin
		update WALLET set Balance = @currentBalance + @insertedTransaction 
		where WalletID = @walletID
	end
	else
	begin
		update WALLET set Balance = @currentBalance - @insertedTransaction 
		where WalletID = @walletID
	end
END
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
create trigger TR_HANDLE_CUSTOMER_ORDER ON CUSTOMER_ORDER for  UPDATE AS
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

-- trigger to set tier -- modify this one
select CustomerID, sum(OrderTotalPrice) as Consumption from CUSTOMER_ORDER where OrderStatus = 'SUCCESS'
group by CustomerID

go
create trigger TR_SET_TIER_FROM_SPEDNING ON CUSTOMER_ORDER for  UPDATE AS
BEGIN
	IF (SELECT COUNT(*) FROM INSERTED WHERE OrderStatus = 'SUCCESS') > 0
	BEGIN
		DECLARE @totalSpending decimal(12,2) = 0;
		DECLARE @customerID int = 0
		DECLARE @tierID int = 1;

		select @customerID = CustomerID from inserted
		select @totalSpending= SUM(OrderTotalPrice) from CUSTOMER_ORDER WHERE CustomerID = @customerID AND OrderStatus = 'SUCCESS';

		SELECT TOP 1 @tierID = TierID FROM TIER 
		WHERE @totalSpending >= TierLevel
		ORDER BY TierLevel DESC
		
		update PERSON set PERSON.TierID = @tierID where PersonID = @customerID
	END
END

-- CUSTOMER_ORDER TABLE

-- trigger to calculate OrderTotalPrice from  DetailCurrentPrice and  DetailQuantity

go
create trigger TR_CALCULATE_TOTAL_PRICE_FROM_ORDER_DETAIL on CUSTOMER_ORDER_DETAIL for insert as
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
-- triger on BOOK_EDITION TO INITIAL STOCK_INVENTORY
GO
CREATE TRIGGER TR_INITIAL_STOCK_INVENTORY ON BOOK_EDITION FOR INSERT AS
BEGIN
	DECLARE @editionID int;
	select @editionID = EditionID from inserted 
	insert into STOCK_INVENTORY (EditionID) values(@editionID)
END
-- TRIGER ON TR_STOCK_INVENTORY TO UPDATE InventoryAvailableStock
GO
CREATE TRIGGER TR_UPDATE_INVENTORY_AVAILABLE_STOCK ON STOCK_INVENTORY FOR UPDATE AS
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
-- restore stock when cancel by customer
GO
CREATE TRIGGER TR_RESTORE_STOCK ON CUSTOMER_ORDER
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @orderID INT = 0;
	DECLARE @orderStatus NVARCHAR(50)

	SELECT	
			@orderID = OrderID, 
			@orderStatus = OrderStatus
	FROM inserted;

	IF(@orderStatus = 'CANCEL BY CUSTOMER')
	BEGIN
		DECLARE @EditionID INT
		DECLARE @DetailQuantity INT

		DECLARE CursorStock CURSOR 
			FOR SELECT EditionID, DetailQuantity 
			FROM CUSTOMER_ORDER_DETAIL
			WHERE OrderID = @orderID
		OPEN CursorStock

		FETCH NEXT FROM cursorProduct INTO @EditionID, @DetailQuantity 

		WHILE @@FETCH_STATUS = 0
		BEGIN
			UPDATE STOCK_INVENTORY
			SET InventoryStockOutTotal = InventoryStockOutTotal - @DetailQuantity 
				WHERE EditionID = @EditionID
			FETCH NEXT FROM cursorProduct INTO @EditionID, @DetailQuantity 
		END

		CLOSE CursorStock
		DEALLOCATE CursorStock
	END
END

--trigger to check start date must be < enddate
go
CREATE TRIGGER TR_CHECK_PROMOTION_DATE ON Promotion for insert,update AS
BEGIN
	if((select PromotionStartDate from inserted) >= (select PromotionEndDate from inserted))
	begin
		raiserror('Promotion start date must be less than promotion end date',16,1)
		rollback tran
	end

END
-- trigger to trim and upper ccase
go
CREATE TRIGGER TR_TRIM_AND_LOWERCASE ON Category for insert,update AS
BEGIN
	declare @categoryName nvarchar(128) = (select lower(CategoryName) from inserted)
	
	WHILE CHARINDEX('  ', @categoryName) > 0
	BEGIN
		SET @categoryName = REPLACE(@categoryName, '  ', ' ')
	END

	update category set CategoryName = @categoryName where CategoryID = (select CategoryID from inserted)
END

-- trigger to check type of transation






--after having asptable in datase. running this


----trigger to set STAFF role
--go
--CREATE TRIGGER TR_INITIAL_STAFF_ROLE ON STAFF for insert AS
--BEGIN
--	DECLARE @AccountID NVARCHAR(128);
--	SELECT @AccountID =  AccountID from INSERTED
--	INSERT INTO AspNetUserRoles(UserId,RoleId) VALUES (@AccountID, 2)
--END
----trigger to set SHIPPER role
--go
--CREATE TRIGGER TR_INITIAL_SHIPPER_ROLE ON SHIPPER for insert AS
--BEGIN
--	DECLARE @AccountID  NVARCHAR(128);
--	SELECT @AccountID =  AccountID from INSERTED
--	INSERT INTO AspNetUserRoles(UserId,RoleId) VALUES (@AccountID, 3)
--END



--select * from wallet
-- drop  trigger

	-- CUSTOMER TABLE
	--drop trigger TR_CALCULATE_BALANCE_FROM_TRANSACTION
	--drop trigger TR_CALCULATE_BALANCE_FROM_CUSTOMER_ORDER_WITH_UPDATE
	--drop trigger TR_SET_TIER_FROM_SPEDNING
	--drop trigger TR_STOCK_RECEIVED_NOTE_DETAIL_WITH_INSERT

	-- CUSTOMER_ORDER TABLE
-- test view
	--create view VIEW_CURRENT_BALANCE as
	
	--select a.CustomerID, (a.CurrentBalance - b.TotalPayByBalance) as Total
	--from
	--(select SUM(TD.TransactionAmount) as CurrentBalance, C.CustomerID from TRANSACTION_DETAIL TD, BANK_ACCOUNT BA, CUSTOMER C
	--where TD.BankAccountID = Ba.BankAccountID and BA.CustomerID = C.CustomerID
	--GROUP BY C.CustomerID) as a
	
	--(select SUM(CD.OrderTotalPrice) as TotalPayByBalance, C.CustomerID from CUSTOMER_ORDER CD, CUSTOMER C 
	--WHERE CD.CustomerID = C.CustomerID AND CD.OrderPaymentMethod = 'CURRENT BALANCE'
	--AND CD.OrderStatus NOT IN('INITIAL', 'CANCEL BY CUSTOMER')
	--GROUP BY C.CustomerID) as b
	--where a.CustomerID = b.CustomerID

	--select * from VIEW_CURRENT_BALANCE
