use BookStoreManager;
--neu database chua co 2 bang duoi thi khoi can chay 2 dong nay
drop table CUSTOMER_ORDER_STATUS
drop table ORDER_STATUS
-------------------------------------------------

drop table CUSTOMER_ORDER_DETAIL
drop table TRANSACTION_DETAILS
drop table SHIP_CONFIRMATION

drop table CUSTOMER_ORDER
----------------------------------------------------
CREATE TABLE CUSTOMER_ORDER(
	OrderID INT PRIMARY KEY IDENTITY(1,1),
	OrderDate DATETIME DEFAULT GETDATE() NOT NULL,
	OrderTotalPrice DECIMAL(12,2) NOT NULL DEFAULT 0,
	OrderShippingMethod NVARCHAR(50) NOT NULL DEFAULT 'SHIPPING',
		CONSTRAINT CHK_OrderShippingMethod
		CHECK(OrderShippingMethod IN ('SHIPPING','RECEIVE AT STORE')),
	OrderPaymentMethod NVARCHAR(50) NOT NULL DEFAULT 'CASH',
		CONSTRAINT CHK_OrderPaymentMethod
		CHECK(OrderPaymentMethod IN ('CASH','CURRENT BALANCE')),
	StaffID INT,
	CustomerID INT not null,
	ShipperID INT,
	FOREIGN KEY (StaffID) REFERENCES PERSON(PersonID),
	FOREIGN KEY (CustomerID) REFERENCES PERSON(PersonID),
	FOREIGN KEY (ShipperID) REFERENCES PERSON(PersonID),
)

create table ORDER_STATUS (
	StatusID int identity(1,1) primary key,
	OrderStatus NVARCHAR(50) NOT NULL
)
--DEFAULT 'INITIAL',
--		CONSTRAINT CHK_OrderStatus
--		CHECK(OrderStatus IN ('INITIAL', 
--		'WAITING',
--		'CANCEL BY CUSTOMER',
--		'PROCESSING',
--		'IS AVAILABLE AT STORE', 
--		'DELIVERING',
--		'SUCCESS',
--		'CANCEL BECAUSE OF MANY FAILED DELIVERING'))
create table CUSTOMER_ORDER_STATUS (
	OrderID int not null,
	StatusID int not null,
	UpdateTime datetime not null,
	CONSTRAINT PK_CUSTOMER_ORDER_STATUS_PRIMARY_KEY PRIMARY KEY (OrderID,StatusID),
	FOREIGN KEY (OrderID) REFERENCES CUSTOMER_ORDER(OrderID),
	FOREIGN KEY (StatusID) REFERENCES ORDER_STATUS(StatusID)
)
CREATE TABLE CUSTOMER_ORDER_DETAIL(
	OrderDetailID int primary key identity(1,1),
	DetailCurrentPrice DECIMAL(12,2),
		CONSTRAINT CHK_DetailCurrentPrice
		CHECK(DetailCurrentPrice >= 1000),
	DetailQuantity INT not null,
	OrderID INT not null,
	EditionID INT not null,
	constraint UQ_EditionID_OrderID unique(EditionID, OrderID),
	FOREIGN KEY (EditionID) REFERENCES BOOK_EDITION(EditionID),
	FOREIGN KEY (OrderID) REFERENCES CUSTOMER_ORDER(OrderID)
)
CREATE TABLE TRANSACTION_DETAILS(
	TransactionID INT PRIMARY KEY IDENTITY(1,1),
	TransactionDate DATETIME NOT NULL DEFAULT GETDATE(),
	TransactionType nvarchar(128) not null
	check(TransactionType in ('Place order', 'Refund', 'Deposit', 'Withdrawal')) ,
	TransactionAmount DECIMAL(14,2),
	WalletID INT ,
	BankAccountID int,
	OrderID INT,
	foreign key(WalletID) references WALLET(WalletID),
	foreign key(BankAccountID) references BANK_ACCOUNT(BankAccountID),
	foreign key(OrderID) references Customer_Order(OrderID),
	CHECK (
	(WalletID IS NOT NULL AND BankAccountID IS NOT NULL and TransactionAmount is not null and OrderID is null and TransactionType in ('Deposit', 'Withdrawal')) 
	OR (WalletID IS not null AND BankAccountID IS NULL and OrderID is not null and TransactionAmount is null and
	 TransactionType in ('Place order', 'Refund')))
)

CREATE TABLE SHIP_CONFIRMATION(
	ConfirmationID INT PRIMARY KEY IDENTITY(1,1),
	ConfirmationDate DATETIME NOT NULL DEFAULT GETDATE(),
	ConfirmationImage NVARCHAR(50) NOT NULL,
	OrderID INT not null,
	ShipperID INT not null,
	constraint UQ_OrderID_ShipperID unique(OrderID,ShipperID),
	FOREIGN KEY (OrderID) REFERENCES CUSTOMER_ORDER(OrderID),
	FOREIGN KEY (ShipperID) REFERENCES PERSON(PersonID)
)

--order_status
SET IDENTITY_INSERT ORDER_STATUS ON

insert into ORDER_STATUS(StatusID, OrderStatus) values (1,'INITIAL'),
(2,'WAITING'),
(3,'CANCEL BY CUSTOMER'),
(4,'PROCESSING'),
(5,'IS AVAILABLE AT STORE'),
(6,'DELIVERING'),
(7,'SUCCESS'),
(8,'CANCEL BECAUSE OF MANY FAILED DELIVERING')