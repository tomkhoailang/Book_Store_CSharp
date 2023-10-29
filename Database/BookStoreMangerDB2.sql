--create database BookStoreManager;

-- INITAL 1: running this section after running the database using register an account in identity
--ALTER TABLE CUSTOMER ADD CONSTRAINT FK_CUSTOMER_AspNetUsers FOREIGN KEY (AccountID) REFERENCES AspNetUsers(Id)
--ALTER TABLE STAFF ADD CONSTRAINT FK_STAFF_AspNetUsers FOREIGN KEY (AccountID) REFERENCES AspNetUsers(Id)
--ALTER TABLE SHIPPER ADD CONSTRAINT FK_SHIPPER_AspNetUsers FOREIGN KEY (AccountID) REFERENCES AspNetUsers(Id)
--ALTER TABLE MANAGER ADD CONSTRAINT FK_MANGER_AspNetUsers FOREIGN KEY (AccountID) REFERENCES AspNetUsers(Id)


use BookStoreManager;

CREATE TABLE ACCOUNT(
	AccountID INT PRIMARY KEY IDENTITY(1,1),
	AccountUserName NVARCHAR(50) NOT NULL UNIQUE,
	AccountPassword NVARCHAR(50) NOT NULL,
	AccountType NVARCHAR(50) DEFAULT 'CUSTOMER' NOT NULL
		CONSTRAINT CHK_AccountType
		CHECK(AccountType IN ('CUSTOMER','STAFF', 'SHIPPER','MANAGER'))
)

CREATE TABLE TIER(
	TierID INT PRIMARY KEY IDENTITY(1,1),
	TierName NVARCHAR(50) UNIQUE,
	TierDiscount DECIMAL(4,2) DEFAULT 0 NOT NULL UNIQUE,
	TierLevel INT NOT NULL DEFAULT 0 UNIQUE
)
CREATE TABLE CUSTOMER(
	CustomerID INT PRIMARY KEY IDENTITY(1,1),
	CustomerName NVARCHAR(50) NOT NULL,
	CustomerPhoneNumber NVARCHAR(10) UNIQUE,
		CONSTRAINT CHK_CustomerPhoneNumber
		CHECK(CustomerPhoneNumber LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	CustomerAddress NVARCHAR(100),
	CustomerEmail NVARCHAR(50) UNIQUE,
		CONSTRAINT CHK_CustomerEmail
		CHECK(CustomerEmail LIKE '____%@__%.__%'),
	CustomerStatus NVARCHAR(50) DEFAULT 'ACTIVE',
		CONSTRAINT CHK_CustomerStatus
		CHECK(CustomerStatus IN('ACTIVE', 'LOCKED')),
	AccountID INT UNIQUE,
	TierID INT NOT NULL DEFAULT 1,
	FOREIGN KEY (AccountID) REFERENCES ACCOUNT(AccountID),
	FOREIGN KEY (TierID) REFERENCES TIER(TierID)
);
CREATE TABLE WALLET(
	WalletID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT UNIQUE,
	Balance DECIMAL(12,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (CustomerID) REFERENCES CUSTOMER(CustomerID)
)
CREATE TABLE BANK_ACCOUNT(
	BankAccountID INT PRIMARY KEY IDENTITY(1,1),
	BankAccountNumber NVARCHAR(16) UNIQUE
		 CONSTRAINT CK_BankAccountNumber
		 CHECK (BankAccountNumber LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	BankAccountName NVARCHAR(50) NOT NULL,
	BankCVC NVARCHAR(3) 
		 CONSTRAINT CK_BankCVC
		 CHECK (BankCVC LIKE '[0-9][0-9][0-9]'),
	CustomerID INT,
	FOREIGN KEY (CustomerID) REFERENCES CUSTOMER(CustomerID),
)
CREATE TABLE TRANSACTION_DETAIL(
	TransactionID INT PRIMARY KEY IDENTITY(1,1),
	TransactionDate DATETIME NOT NULL DEFAULT GETDATE(),
	TransactionAmount DECIMAL(12,2) NOT NULL,
	BankAccountID INT,
	FOREIGN KEY (BankAccountID) REFERENCES BANK_ACCOUNT(BankAccountID),
)
CREATE TABLE STAFF(
	StaffID INT PRIMARY KEY IDENTITY(1,1),
	StaffName NVARCHAR(50) NOT NULL,
	StaffPhoneNumber NVARCHAR(10) UNIQUE,
		CONSTRAINT CHK_StaffPhoneNumber
		CHECK(StaffPhoneNumber LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	StaffAddress NVARCHAR(100),
	StaffEmail NVARCHAR(50) UNIQUE,
		CONSTRAINT CHK_StaffEmail
		CHECK(StaffEmail LIKE '____%@__%.__%'),
	StaffStatus NVARCHAR(50) DEFAULT 'ACTIVE',
		CONSTRAINT CHK_StaffStatus
		CHECK(StaffStatus IN('ACTIVE', 'LOCKED')),
	AccountID INT UNIQUE,
	FOREIGN KEY (AccountID) REFERENCES ACCOUNT(AccountID)
);
CREATE TABLE SHIPPER(
	ShipperID INT PRIMARY KEY IDENTITY(1,1),
	ShipperName NVARCHAR(50) NOT NULL,
	ShipperPhoneNumber NVARCHAR(10) UNIQUE,
		CONSTRAINT CHK_ShipperPhoneNumber
		CHECK(ShipperPhoneNumber LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	ShipperAddress NVARCHAR(100),
	ShipperEmail NVARCHAR(50) UNIQUE,
		CONSTRAINT CHK_ShipperEmail
		CHECK(ShipperEmail LIKE '____%@__%.__%'),
	ShipperStatus NVARCHAR(50) DEFAULT 'ACTIVE',
		CONSTRAINT CHK_ShipperStatus
		CHECK(ShipperStatus IN('ACTIVE', 'LOCKED')),
	AccountID INT UNIQUE,
	FOREIGN KEY (AccountID) REFERENCES ACCOUNT(AccountID)
);
CREATE TABLE MANAGER(
	ManagerID INT PRIMARY KEY IDENTITY(1,1),
	ManagerName NVARCHAR(50) NOT NULL,
	ManagerPhoneNumber NVARCHAR(10) UNIQUE,
		CONSTRAINT CHK_ManagerPhoneNumber
		CHECK(ManagerPhoneNumber LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	ManagerAddress NVARCHAR(100),
	ManagerEmail NVARCHAR(50) UNIQUE,
		CONSTRAINT CHK_ManagerEmail
		CHECK(ManagerEmail LIKE '____%@__%.__%'),
	AccountID INT UNIQUE,
	FOREIGN KEY (AccountID) REFERENCES ACCOUNT(AccountID)
)

CREATE TABLE PUBLISHER(
	PublisherID INT PRIMARY KEY IDENTITY(1,1),
	PublisherName NVARCHAR(100) NOT NULL UNIQUE,
	PublisherDescription NVARCHAR(500),
	PublisherImage NVARCHAR(200)
)
CREATE TABLE CATEGORY(
	CategoryID INT PRIMARY KEY IDENTITY(1,1),
	CategoryName NVARCHAR(100) NOT NULL UNIQUE,
	CategoryDescription NVARCHAR(100) NOT NULL
)
CREATE TABLE PROMOTION(
	PromotionID INT PRIMARY KEY IDENTITY(1,1),
	PromotionName NVARCHAR(200) NOT NULL,
	PromotionDiscount DECIMAL(4,2) NOT NULL,
	PromotionStartDate DATETIME DEFAULT GETDATE() NOT NULL,
	PromotionEndDate DATETIME DEFAULT DATEADD(DAY,14,GETDATE()) NOT NULL,
	PromotionIsValid bit default 0 NOT NULL
)
alter table PROMOTION add PromotionDetails TEXT;
CREATE TABLE BOOK_COLLECTION(
	BookCollectionID INT PRIMARY KEY IDENTITY(1,1),
	BookCollectionName NVARCHAR(200) NOT NULL
)
alter table book_collection add constraint UQ_BookCollectionName Unique(BookCollectionName)

CREATE TABLE BOOK_EDITION(
	EditionID INT PRIMARY KEY IDENTITY(1,1),
	EditionPrice DECIMAL(12,2) NOT NULL,
		CONSTRAINT CHK_EditionPrice
		CHECK(EditionPrice >= 1000),
	EditionDescription NVARCHAR(500),
	EditionYear NVARCHAR(4) NOT NULL ,
		CONSTRAINT CHK_EditionYear
		CHECK(EditionYear LIKE '[0-9][0-9][0-9][0-9]'),
	EditionPageCount DECIMAL(5) NOT NULL,
	EditionDiscount DECIMAL(4,2) DEFAULT 0,
	EditionName NVARCHAR(200) NOT NULL,
	EditionAuthor NVARCHAR(100) NOT NULL,
	EditionStatus NVARCHAR(50) NOT NULL default 'OUT OF STOCK',
		CONSTRAINT CHK_EditionStatus
		CHECK(EditionStatus IN('ACTIVE','OUT OF STOCK')),
	CategoryID INT,
	FOREIGN KEY (CategoryID) REFERENCES CATEGORY(CategoryID)
)
alter table BOOK_EDITION add BookCollectionID int;
alter table BOOK_EDITION add foreign key (BookCollectionID) REFERENCES BOOK_COLLECTION(BookCollectionID)

CREATE TABLE BOOK_PROMOTION(
	PromotionID INT,
	EditionID INT,
	PRIMARY KEY(PromotionID, EditionID),
	FOREIGN KEY (PromotionID) REFERENCES PROMOTION(PromotionID),
	FOREIGN KEY (EditionID) REFERENCES BOOK_EDITION(EditionID)
)
CREATE TABLE STOCK_RECEIVED_NOTE(
	StockReceivedNoteID  INT PRIMARY KEY IDENTITY(1,1),
	StockReceivedNoteDate DATETIME NOT NULL DEFAULT GETDATE(),
	PublisherID INT,
	ManagerID INT default 1,
	FOREIGN KEY (PublisherID) REFERENCES PUBLISHER(PublisherID),
	FOREIGN KEY (ManagerID) REFERENCES MANAGER(ManagerID)
)
CREATE TABLE STOCK_RECEIVED_NOTE_DETAIL(
	NoteDetailQuantity INT NOT NULL DEFAULT 0,
		CONSTRAINT CHK_NoteDetailQuantity
		CHECK(NoteDetailQuantity >=0),
	NoteDetailPrice DECIMAL(12,2) NOT NULL,
		CONSTRAINT CHK_NoteDetailPrice
		CHECK(NoteDetailPrice >= 1000),
	EditionID INT,
	StockReceivedNoteID INT,
	PRIMARY KEY(EditionID, StockReceivedNoteID),
	FOREIGN KEY (EditionID) REFERENCES BOOK_EDITION(EditionID),
	FOREIGN KEY (StockReceivedNoteID) REFERENCES STOCK_RECEIVED_NOTE(StockReceivedNoteID)

)
CREATE TABLE STOCK_INVENTORY(
	InventoryStockInTotal INT NOT NULL DEFAULT 0,
		CONSTRAINT CHK_InventoryStockInTotal
		CHECK(InventoryStockInTotal >=0),
	InventoryStockOutTotal INT NOT NULL DEFAULT 0,
		CONSTRAINT CHK_InventoryStockOutTotal
		CHECK(InventoryStockOutTotal >=0),
	InventoryAvailableStock INT NOT NULL DEFAULT 0,
	EditionID INT NOT NULL,
	PRIMARY KEY(EditionID),
	FOREIGN KEY (EditionID) REFERENCES BOOK_EDITION(EditionID)
)
CREATE TABLE BOOK_REVIEW(
	ReviewID INT PRIMARY KEY IDENTITY(1,1),
	ReviewDescription NVARCHAR(1000) NOT NULL,
	ReviewDate DATETIME NOT NULL DEFAULT GETDATE(),
	CustomerID int,
	EditionID INT,
	CONSTRAINT UQ_CustomerEdition UNIQUE (CustomerID, EditionID),
	FOREIGN KEY (EditionID) REFERENCES BOOK_EDITION(EditionID),
	FOREIGN KEY (CustomerID) REFERENCES CUSTOMER(CustomerID)
)
alter table BOOK_REVIEW add ReviewRating int not null
alter table BOOK_REVIEW add constraint CHK_Rating check(ReviewRating between 1 and 5)

CREATE TABLE BOOK_EDITION_IMAGE(
	EditionImageID INT PRIMARY KEY IDENTITY(1,1),
	EditionImage NVARCHAR(100) NOT NULL,
	EditionID INT,
	FOREIGN KEY (EditionID) REFERENCES BOOK_EDITION(EditionID)
)
CREATE TABLE CUSTOMER_ORDER(
	OrderID INT PRIMARY KEY IDENTITY(1,1),
	OrderDate DATETIME DEFAULT GETDATE() NOT NULL,
	OrderTotalPrice DECIMAL(12,2) NOT NULL DEFAULT 0,
	OrderStatus NVARCHAR(50) NOT NULL DEFAULT 'INITIAL',
		CONSTRAINT CHK_OrderStatus
		CHECK(OrderStatus IN ('INITIAL', 'WAITING','CANCEL BY CUSTOMER','PROCESSING','IS AVAILABLE AT STORE', 'DELIVERING','SUCCESS','CANCEL BECAUSE OF MANY FAILED DELIVERING')),
	OrderShippingMethod NVARCHAR(50) NOT NULL DEFAULT 'SHIPPING',
		CONSTRAINT CHK_OrderShippingMethod
		CHECK(OrderShippingMethod IN ('SHIPPING','RECEIVE AT STORE')),
	OrderPaymentMethod NVARCHAR(50) NOT NULL DEFAULT 'CASH',
		CONSTRAINT CHK_OrderPaymentMethod
		CHECK(OrderPaymentMethod IN ('CASH','CURRENT BALANCE')),
	StaffID INT,
	CustomerID INT,
	ShipperID INT,
	FOREIGN KEY (StaffID) REFERENCES STAFF(StaffID),
	FOREIGN KEY (CustomerID) REFERENCES CUSTOMER(CustomerID),
	FOREIGN KEY (ShipperID) REFERENCES SHIPPER(ShipperID)
)
CREATE TABLE CUSTOMER_ORDER_DETAIL(
	DetailCurrentPrice DECIMAL(12,2),
		CONSTRAINT CHK_DetailCurrentPrice
		CHECK(DetailCurrentPrice >= 1000),
	DetailQuantity INT,
	OrderID INT,
	EditionID INT,
	FOREIGN KEY (EditionID) REFERENCES BOOK_EDITION(EditionID),
	FOREIGN KEY (OrderID) REFERENCES CUSTOMER_ORDER(OrderID)
)
CREATE TABLE SHIP_CONFIRMATION(
	ConfirmationDate DATETIME NOT NULL DEFAULT GETDATE(),
	ConfirmationImage NVARCHAR(50) NOT NULL,
	OrderID INT,
	ShipperID INT,
	FOREIGN KEY (OrderID) REFERENCES CUSTOMER_ORDER(OrderID),
	FOREIGN KEY (ShipperID) REFERENCES SHIPPER(ShipperID)
)
--index
CREATE UNIQUE NONCLUSTERED INDEX idx_Customer_Phone_Number
ON CUSTOMER(CustomerPhoneNumber)
WHERE CustomerPhoneNumber IS NOT NULL;

CREATE UNIQUE NONCLUSTERED INDEX idx_Staff_Phone_Number
ON STAFF(StaffPhoneNumber)
WHERE StaffPhoneNumber IS NOT NULL;

CREATE UNIQUE NONCLUSTERED INDEX idx_Manager_Phone_Number
ON MANAGER(ManagerPhoneNumber)
WHERE ManagerPhoneNumber IS NOT NULL;

CREATE UNIQUE NONCLUSTERED INDEX idx_Shipper_Phone_Number
ON SHIPPER(ShipperPhoneNumber)
WHERE ShipperPhoneNumber IS NOT NULL;