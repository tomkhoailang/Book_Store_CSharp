
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/27/2023 08:31:50
-- Generated from EDMX file: D:\UTC2\Hoc_ky_5\BTLProject\Main Project\Book_Store_CSharp\WebApplication2\Models\BookStoreManger.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BookStoreManager];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__BANK_ACCO__Walle__4D94879B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BANK_ACCOUNT] DROP CONSTRAINT [FK__BANK_ACCO__Walle__4D94879B];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_CATE__Categ__70DDC3D8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_CATEGORY] DROP CONSTRAINT [FK__BOOK_CATE__Categ__70DDC3D8];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_CATE__Editi__71D1E811]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_CATEGORY] DROP CONSTRAINT [FK__BOOK_CATE__Editi__71D1E811];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_EDIT__Editi__123EB7A3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_EDITION_IMAGE] DROP CONSTRAINT [FK__BOOK_EDIT__Editi__123EB7A3];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_PROM__Editi__75A278F5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_PROMOTION] DROP CONSTRAINT [FK__BOOK_PROM__Editi__75A278F5];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_PROM__Promo__74AE54BC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_PROMOTION] DROP CONSTRAINT [FK__BOOK_PROM__Promo__74AE54BC];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_REVI__Custo__0F624AF8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_REVIEW] DROP CONSTRAINT [FK__BOOK_REVI__Custo__0F624AF8];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_REVI__Editi__0E6E26BF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_REVIEW] DROP CONSTRAINT [FK__BOOK_REVI__Editi__0E6E26BF];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Custo__1DB06A4F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER] DROP CONSTRAINT [FK__CUSTOMER___Custo__1DB06A4F];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Editi__236943A5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER_DETAIL] DROP CONSTRAINT [FK__CUSTOMER___Editi__236943A5];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Order__245D67DE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER_DETAIL] DROP CONSTRAINT [FK__CUSTOMER___Order__245D67DE];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Shipp__1EA48E88]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER] DROP CONSTRAINT [FK__CUSTOMER___Shipp__1EA48E88];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Staff__1CBC4616]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER] DROP CONSTRAINT [FK__CUSTOMER___Staff__1CBC4616];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER__TierID__4316F928]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER] DROP CONSTRAINT [FK__CUSTOMER__TierID__4316F928];
GO
IF OBJECT_ID(N'[dbo].[FK__SHIP_CONF__Order__282DF8C2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SHIP_CONFIRMATION] DROP CONSTRAINT [FK__SHIP_CONF__Order__282DF8C2];
GO
IF OBJECT_ID(N'[dbo].[FK__SHIP_CONF__Shipp__29221CFB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SHIP_CONFIRMATION] DROP CONSTRAINT [FK__SHIP_CONF__Shipp__29221CFB];
GO
IF OBJECT_ID(N'[dbo].[FK__STOCK_INV__Editi__09A971A2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STOCK_INVENTORY] DROP CONSTRAINT [FK__STOCK_INV__Editi__09A971A2];
GO
IF OBJECT_ID(N'[dbo].[FK__STOCK_REC__Editi__01142BA1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL] DROP CONSTRAINT [FK__STOCK_REC__Editi__01142BA1];
GO
IF OBJECT_ID(N'[dbo].[FK__STOCK_REC__Manag__7B5B524B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE] DROP CONSTRAINT [FK__STOCK_REC__Manag__7B5B524B];
GO
IF OBJECT_ID(N'[dbo].[FK__STOCK_REC__Publi__7A672E12]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE] DROP CONSTRAINT [FK__STOCK_REC__Publi__7A672E12];
GO
IF OBJECT_ID(N'[dbo].[FK__STOCK_REC__Stock__02084FDA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL] DROP CONSTRAINT [FK__STOCK_REC__Stock__02084FDA];
GO
IF OBJECT_ID(N'[dbo].[FK__TRANSACTI__BankA__5165187F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TRANSACTION_DETAIL] DROP CONSTRAINT [FK__TRANSACTI__BankA__5165187F];
GO
IF OBJECT_ID(N'[dbo].[FK__WALLET__Customer__47DBAE45]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WALLET] DROP CONSTRAINT [FK__WALLET__Customer__47DBAE45];
GO
IF OBJECT_ID(N'[dbo].[FK_BookCollectionID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_EDITION] DROP CONSTRAINT [FK_BookCollectionID];
GO
IF OBJECT_ID(N'[dbo].[FK_CUSTOMER_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER] DROP CONSTRAINT [FK_CUSTOMER_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_MANGER_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MANAGER] DROP CONSTRAINT [FK_MANGER_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_SHIPPER_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SHIPPER] DROP CONSTRAINT [FK_SHIPPER_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_STAFF_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STAFF] DROP CONSTRAINT [FK_STAFF_AspNetUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[BANK_ACCOUNT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BANK_ACCOUNT];
GO
IF OBJECT_ID(N'[dbo].[BOOK_CATEGORY]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BOOK_CATEGORY];
GO
IF OBJECT_ID(N'[dbo].[BOOK_COLLECTION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BOOK_COLLECTION];
GO
IF OBJECT_ID(N'[dbo].[BOOK_EDITION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BOOK_EDITION];
GO
IF OBJECT_ID(N'[dbo].[BOOK_EDITION_IMAGE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BOOK_EDITION_IMAGE];
GO
IF OBJECT_ID(N'[dbo].[BOOK_PROMOTION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BOOK_PROMOTION];
GO
IF OBJECT_ID(N'[dbo].[BOOK_REVIEW]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BOOK_REVIEW];
GO
IF OBJECT_ID(N'[dbo].[CATEGORY]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CATEGORY];
GO
IF OBJECT_ID(N'[dbo].[CUSTOMER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CUSTOMER];
GO
IF OBJECT_ID(N'[dbo].[CUSTOMER_ORDER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CUSTOMER_ORDER];
GO
IF OBJECT_ID(N'[dbo].[CUSTOMER_ORDER_DETAIL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CUSTOMER_ORDER_DETAIL];
GO
IF OBJECT_ID(N'[dbo].[MANAGER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MANAGER];
GO
IF OBJECT_ID(N'[dbo].[PROMOTION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PROMOTION];
GO
IF OBJECT_ID(N'[dbo].[PUBLISHER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PUBLISHER];
GO
IF OBJECT_ID(N'[dbo].[SHIP_CONFIRMATION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SHIP_CONFIRMATION];
GO
IF OBJECT_ID(N'[dbo].[SHIPPER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SHIPPER];
GO
IF OBJECT_ID(N'[dbo].[STAFF]', 'U') IS NOT NULL
    DROP TABLE [dbo].[STAFF];
GO
IF OBJECT_ID(N'[dbo].[STOCK_INVENTORY]', 'U') IS NOT NULL
    DROP TABLE [dbo].[STOCK_INVENTORY];
GO
IF OBJECT_ID(N'[dbo].[STOCK_RECEIVED_NOTE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[STOCK_RECEIVED_NOTE];
GO
IF OBJECT_ID(N'[dbo].[STOCK_RECEIVED_NOTE_DETAIL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TIER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TIER];
GO
IF OBJECT_ID(N'[dbo].[TRANSACTION_DETAIL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TRANSACTION_DETAIL];
GO
IF OBJECT_ID(N'[dbo].[WALLET]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WALLET];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'BANK_ACCOUNT'
CREATE TABLE [dbo].[BANK_ACCOUNT] (
    [BankAccountID] int IDENTITY(1,1) NOT NULL,
    [BankAccountNumber] nvarchar(16)  NULL,
    [BankAccountName] nvarchar(50)  NOT NULL,
    [BankCVC] nvarchar(3)  NULL,
    [WalletID] int  NULL
);
GO

-- Creating table 'BOOK_COLLECTION'
CREATE TABLE [dbo].[BOOK_COLLECTION] (
    [BookCollectionID] int IDENTITY(1,1) NOT NULL,
    [BookCollectionName] nvarchar(200)  NOT NULL
);
GO

-- Creating table 'BOOK_EDITION'
CREATE TABLE [dbo].[BOOK_EDITION] (
    [EditionID] int IDENTITY(1,1) NOT NULL,
    [EditionPrice] decimal(12,2)  NOT NULL,
    [EditionDescription] nvarchar(500)  NULL,
    [EditionYear] nvarchar(4)  NOT NULL,
    [EditionPageCount] int  NOT NULL,
    [EditionName] nvarchar(200)  NOT NULL,
    [EditionAuthor] nvarchar(100)  NOT NULL,
    [BookCollectionID] int  NULL
);
GO

-- Creating table 'BOOK_EDITION_IMAGE'
CREATE TABLE [dbo].[BOOK_EDITION_IMAGE] (
    [EditionImageID] int IDENTITY(1,1) NOT NULL,
    [EditionImage] nvarchar(100)  NOT NULL,
    [EditionID] int  NULL
);
GO

-- Creating table 'BOOK_REVIEW'
CREATE TABLE [dbo].[BOOK_REVIEW] (
    [ReviewID] int IDENTITY(1,1) NOT NULL,
    [ReviewDescription] nvarchar(1000)  NOT NULL,
    [ReviewDate] datetime  NOT NULL,
    [CustomerID] int  NULL,
    [EditionID] int  NULL,
    [ReviewRating] int  NOT NULL
);
GO

-- Creating table 'CATEGORies'
CREATE TABLE [dbo].[CATEGORies] (
    [CategoryID] int IDENTITY(1,1) NOT NULL,
    [CategoryName] nvarchar(100)  NOT NULL,
    [CategoryDescription] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'CUSTOMERs'
CREATE TABLE [dbo].[CUSTOMERs] (
    [CustomerID] int IDENTITY(1,1) NOT NULL,
    [CustomerName] nvarchar(50)  NOT NULL,
    [CustomerPhoneNumber] nvarchar(10)  NULL,
    [CustomerAddress] nvarchar(100)  NULL,
    [CustomerStatus] nvarchar(50)  NULL,
    [AccountID] nvarchar(128)  NULL,
    [TierID] int  NOT NULL
);
GO

-- Creating table 'CUSTOMER_ORDER'
CREATE TABLE [dbo].[CUSTOMER_ORDER] (
    [OrderID] int IDENTITY(1,1) NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [OrderTotalPrice] decimal(12,2)  NOT NULL,
    [OrderStatus] nvarchar(50)  NOT NULL,
    [OrderShippingMethod] nvarchar(50)  NOT NULL,
    [OrderPaymentMethod] nvarchar(50)  NOT NULL,
    [StaffID] int  NULL,
    [CustomerID] int  NULL,
    [ShipperID] int  NULL
);
GO

-- Creating table 'CUSTOMER_ORDER_DETAIL'
CREATE TABLE [dbo].[CUSTOMER_ORDER_DETAIL] (
    [DetailCurrentPrice] decimal(12,2)  NULL,
    [DetailQuantity] int  NOT NULL,
    [OrderID] int  NOT NULL,
    [EditionID] int  NOT NULL
);
GO

-- Creating table 'MANAGERs'
CREATE TABLE [dbo].[MANAGERs] (
    [ManagerID] int IDENTITY(1,1) NOT NULL,
    [ManagerName] nvarchar(50)  NOT NULL,
    [ManagerPhoneNumber] nvarchar(10)  NULL,
    [ManagerAddress] nvarchar(100)  NULL,
    [AccountID] nvarchar(128)  NULL
);
GO

-- Creating table 'PROMOTIONs'
CREATE TABLE [dbo].[PROMOTIONs] (
    [PromotionID] int IDENTITY(1,1) NOT NULL,
    [PromotionName] nvarchar(200)  NOT NULL,
    [PromotionDiscount] decimal(4,2)  NOT NULL,
    [PromotionStartDate] datetime  NOT NULL,
    [PromotionEndDate] datetime  NOT NULL,
    [PromotionDetails] varchar(max)  NULL
);
GO

-- Creating table 'PUBLISHERs'
CREATE TABLE [dbo].[PUBLISHERs] (
    [PublisherID] int IDENTITY(1,1) NOT NULL,
    [PublisherName] nvarchar(100)  NOT NULL,
    [PublisherDescription] nvarchar(500)  NULL,
    [PublisherImage] nvarchar(200)  NULL
);
GO

-- Creating table 'SHIP_CONFIRMATION'
CREATE TABLE [dbo].[SHIP_CONFIRMATION] (
    [ShipConfirmationID] int IDENTITY(1,1) NOT NULL,
    [ConfirmationDate] datetime  NOT NULL,
    [ConfirmationImage] nvarchar(50)  NOT NULL,
    [OrderID] int  NULL,
    [ShipperID] int  NULL
);
GO

-- Creating table 'SHIPPERs'
CREATE TABLE [dbo].[SHIPPERs] (
    [ShipperID] int IDENTITY(1,1) NOT NULL,
    [ShipperName] nvarchar(50)  NOT NULL,
    [ShipperPhoneNumber] nvarchar(10)  NULL,
    [ShipperAddress] nvarchar(100)  NULL,
    [ShipperStatus] nvarchar(50)  NULL,
    [AccountID] nvarchar(128)  NULL
);
GO

-- Creating table 'STAFFs'
CREATE TABLE [dbo].[STAFFs] (
    [StaffID] int IDENTITY(1,1) NOT NULL,
    [StaffName] nvarchar(50)  NOT NULL,
    [StaffPhoneNumber] nvarchar(10)  NULL,
    [StaffAddress] nvarchar(100)  NULL,
    [StaffStatus] nvarchar(50)  NULL,
    [AccountID] nvarchar(128)  NULL
);
GO

-- Creating table 'STOCK_INVENTORY'
CREATE TABLE [dbo].[STOCK_INVENTORY] (
    [InventoryStockInTotal] int  NOT NULL,
    [InventoryStockOutTotal] int  NOT NULL,
    [InventoryAvailableStock] int  NOT NULL,
    [EditionID] int  NOT NULL
);
GO

-- Creating table 'STOCK_RECEIVED_NOTE'
CREATE TABLE [dbo].[STOCK_RECEIVED_NOTE] (
    [StockReceivedNoteID] int IDENTITY(1,1) NOT NULL,
    [StockReceivedNoteDate] datetime  NOT NULL,
    [PublisherID] int  NULL,
    [ManagerID] int  NULL
);
GO

-- Creating table 'STOCK_RECEIVED_NOTE_DETAIL'
CREATE TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL] (
    [NoteDetailQuantity] int  NOT NULL,
    [NoteDetailPrice] decimal(12,2)  NOT NULL,
    [EditionID] int  NOT NULL,
    [StockReceivedNoteID] int  NOT NULL
);
GO

-- Creating table 'TIERs'
CREATE TABLE [dbo].[TIERs] (
    [TierID] int IDENTITY(1,1) NOT NULL,
    [TierName] nvarchar(50)  NULL,
    [TierDiscount] decimal(4,2)  NOT NULL,
    [TierLevel] int  NOT NULL
);
GO

-- Creating table 'TRANSACTION_DETAIL'
CREATE TABLE [dbo].[TRANSACTION_DETAIL] (
    [TransactionID] int IDENTITY(1,1) NOT NULL,
    [TransactionDate] datetime  NOT NULL,
    [TransactionAmount] decimal(12,2)  NOT NULL,
    [BankAccountID] int  NULL
);
GO

-- Creating table 'WALLETs'
CREATE TABLE [dbo].[WALLETs] (
    [WalletID] int IDENTITY(1,1) NOT NULL,
    [CustomerID] int  NULL,
    [Balance] decimal(12,2)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'BOOK_CATEGORY'
CREATE TABLE [dbo].[BOOK_CATEGORY] (
    [CATEGORies_CategoryID] int  NOT NULL,
    [BOOK_EDITION_EditionID] int  NOT NULL
);
GO

-- Creating table 'BOOK_PROMOTION'
CREATE TABLE [dbo].[BOOK_PROMOTION] (
    [BOOK_EDITION_EditionID] int  NOT NULL,
    [PROMOTIONs_PromotionID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [BankAccountID] in table 'BANK_ACCOUNT'
ALTER TABLE [dbo].[BANK_ACCOUNT]
ADD CONSTRAINT [PK_BANK_ACCOUNT]
    PRIMARY KEY CLUSTERED ([BankAccountID] ASC);
GO

-- Creating primary key on [BookCollectionID] in table 'BOOK_COLLECTION'
ALTER TABLE [dbo].[BOOK_COLLECTION]
ADD CONSTRAINT [PK_BOOK_COLLECTION]
    PRIMARY KEY CLUSTERED ([BookCollectionID] ASC);
GO

-- Creating primary key on [EditionID] in table 'BOOK_EDITION'
ALTER TABLE [dbo].[BOOK_EDITION]
ADD CONSTRAINT [PK_BOOK_EDITION]
    PRIMARY KEY CLUSTERED ([EditionID] ASC);
GO

-- Creating primary key on [EditionImageID] in table 'BOOK_EDITION_IMAGE'
ALTER TABLE [dbo].[BOOK_EDITION_IMAGE]
ADD CONSTRAINT [PK_BOOK_EDITION_IMAGE]
    PRIMARY KEY CLUSTERED ([EditionImageID] ASC);
GO

-- Creating primary key on [ReviewID] in table 'BOOK_REVIEW'
ALTER TABLE [dbo].[BOOK_REVIEW]
ADD CONSTRAINT [PK_BOOK_REVIEW]
    PRIMARY KEY CLUSTERED ([ReviewID] ASC);
GO

-- Creating primary key on [CategoryID] in table 'CATEGORies'
ALTER TABLE [dbo].[CATEGORies]
ADD CONSTRAINT [PK_CATEGORies]
    PRIMARY KEY CLUSTERED ([CategoryID] ASC);
GO

-- Creating primary key on [CustomerID] in table 'CUSTOMERs'
ALTER TABLE [dbo].[CUSTOMERs]
ADD CONSTRAINT [PK_CUSTOMERs]
    PRIMARY KEY CLUSTERED ([CustomerID] ASC);
GO

-- Creating primary key on [OrderID] in table 'CUSTOMER_ORDER'
ALTER TABLE [dbo].[CUSTOMER_ORDER]
ADD CONSTRAINT [PK_CUSTOMER_ORDER]
    PRIMARY KEY CLUSTERED ([OrderID] ASC);
GO

-- Creating primary key on [OrderID], [EditionID] in table 'CUSTOMER_ORDER_DETAIL'
ALTER TABLE [dbo].[CUSTOMER_ORDER_DETAIL]
ADD CONSTRAINT [PK_CUSTOMER_ORDER_DETAIL]
    PRIMARY KEY CLUSTERED ([OrderID], [EditionID] ASC);
GO

-- Creating primary key on [ManagerID] in table 'MANAGERs'
ALTER TABLE [dbo].[MANAGERs]
ADD CONSTRAINT [PK_MANAGERs]
    PRIMARY KEY CLUSTERED ([ManagerID] ASC);
GO

-- Creating primary key on [PromotionID] in table 'PROMOTIONs'
ALTER TABLE [dbo].[PROMOTIONs]
ADD CONSTRAINT [PK_PROMOTIONs]
    PRIMARY KEY CLUSTERED ([PromotionID] ASC);
GO

-- Creating primary key on [PublisherID] in table 'PUBLISHERs'
ALTER TABLE [dbo].[PUBLISHERs]
ADD CONSTRAINT [PK_PUBLISHERs]
    PRIMARY KEY CLUSTERED ([PublisherID] ASC);
GO

-- Creating primary key on [ShipConfirmationID] in table 'SHIP_CONFIRMATION'
ALTER TABLE [dbo].[SHIP_CONFIRMATION]
ADD CONSTRAINT [PK_SHIP_CONFIRMATION]
    PRIMARY KEY CLUSTERED ([ShipConfirmationID] ASC);
GO

-- Creating primary key on [ShipperID] in table 'SHIPPERs'
ALTER TABLE [dbo].[SHIPPERs]
ADD CONSTRAINT [PK_SHIPPERs]
    PRIMARY KEY CLUSTERED ([ShipperID] ASC);
GO

-- Creating primary key on [StaffID] in table 'STAFFs'
ALTER TABLE [dbo].[STAFFs]
ADD CONSTRAINT [PK_STAFFs]
    PRIMARY KEY CLUSTERED ([StaffID] ASC);
GO

-- Creating primary key on [EditionID] in table 'STOCK_INVENTORY'
ALTER TABLE [dbo].[STOCK_INVENTORY]
ADD CONSTRAINT [PK_STOCK_INVENTORY]
    PRIMARY KEY CLUSTERED ([EditionID] ASC);
GO

-- Creating primary key on [StockReceivedNoteID] in table 'STOCK_RECEIVED_NOTE'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE]
ADD CONSTRAINT [PK_STOCK_RECEIVED_NOTE]
    PRIMARY KEY CLUSTERED ([StockReceivedNoteID] ASC);
GO

-- Creating primary key on [EditionID], [StockReceivedNoteID] in table 'STOCK_RECEIVED_NOTE_DETAIL'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL]
ADD CONSTRAINT [PK_STOCK_RECEIVED_NOTE_DETAIL]
    PRIMARY KEY CLUSTERED ([EditionID], [StockReceivedNoteID] ASC);
GO

-- Creating primary key on [TierID] in table 'TIERs'
ALTER TABLE [dbo].[TIERs]
ADD CONSTRAINT [PK_TIERs]
    PRIMARY KEY CLUSTERED ([TierID] ASC);
GO

-- Creating primary key on [TransactionID] in table 'TRANSACTION_DETAIL'
ALTER TABLE [dbo].[TRANSACTION_DETAIL]
ADD CONSTRAINT [PK_TRANSACTION_DETAIL]
    PRIMARY KEY CLUSTERED ([TransactionID] ASC);
GO

-- Creating primary key on [WalletID] in table 'WALLETs'
ALTER TABLE [dbo].[WALLETs]
ADD CONSTRAINT [PK_WALLETs]
    PRIMARY KEY CLUSTERED ([WalletID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- Creating primary key on [CATEGORies_CategoryID], [BOOK_EDITION_EditionID] in table 'BOOK_CATEGORY'
ALTER TABLE [dbo].[BOOK_CATEGORY]
ADD CONSTRAINT [PK_BOOK_CATEGORY]
    PRIMARY KEY CLUSTERED ([CATEGORies_CategoryID], [BOOK_EDITION_EditionID] ASC);
GO

-- Creating primary key on [BOOK_EDITION_EditionID], [PROMOTIONs_PromotionID] in table 'BOOK_PROMOTION'
ALTER TABLE [dbo].[BOOK_PROMOTION]
ADD CONSTRAINT [PK_BOOK_PROMOTION]
    PRIMARY KEY CLUSTERED ([BOOK_EDITION_EditionID], [PROMOTIONs_PromotionID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AccountID] in table 'CUSTOMERs'
ALTER TABLE [dbo].[CUSTOMERs]
ADD CONSTRAINT [FK_CUSTOMER_AspNetUsers]
    FOREIGN KEY ([AccountID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CUSTOMER_AspNetUsers'
CREATE INDEX [IX_FK_CUSTOMER_AspNetUsers]
ON [dbo].[CUSTOMERs]
    ([AccountID]);
GO

-- Creating foreign key on [AccountID] in table 'MANAGERs'
ALTER TABLE [dbo].[MANAGERs]
ADD CONSTRAINT [FK_MANGER_AspNetUsers]
    FOREIGN KEY ([AccountID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MANGER_AspNetUsers'
CREATE INDEX [IX_FK_MANGER_AspNetUsers]
ON [dbo].[MANAGERs]
    ([AccountID]);
GO

-- Creating foreign key on [AccountID] in table 'SHIPPERs'
ALTER TABLE [dbo].[SHIPPERs]
ADD CONSTRAINT [FK_SHIPPER_AspNetUsers]
    FOREIGN KEY ([AccountID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SHIPPER_AspNetUsers'
CREATE INDEX [IX_FK_SHIPPER_AspNetUsers]
ON [dbo].[SHIPPERs]
    ([AccountID]);
GO

-- Creating foreign key on [AccountID] in table 'STAFFs'
ALTER TABLE [dbo].[STAFFs]
ADD CONSTRAINT [FK_STAFF_AspNetUsers]
    FOREIGN KEY ([AccountID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_STAFF_AspNetUsers'
CREATE INDEX [IX_FK_STAFF_AspNetUsers]
ON [dbo].[STAFFs]
    ([AccountID]);
GO

-- Creating foreign key on [WalletID] in table 'BANK_ACCOUNT'
ALTER TABLE [dbo].[BANK_ACCOUNT]
ADD CONSTRAINT [FK__BANK_ACCO__Walle__4D94879B]
    FOREIGN KEY ([WalletID])
    REFERENCES [dbo].[WALLETs]
        ([WalletID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BANK_ACCO__Walle__4D94879B'
CREATE INDEX [IX_FK__BANK_ACCO__Walle__4D94879B]
ON [dbo].[BANK_ACCOUNT]
    ([WalletID]);
GO

-- Creating foreign key on [BankAccountID] in table 'TRANSACTION_DETAIL'
ALTER TABLE [dbo].[TRANSACTION_DETAIL]
ADD CONSTRAINT [FK__TRANSACTI__BankA__5165187F]
    FOREIGN KEY ([BankAccountID])
    REFERENCES [dbo].[BANK_ACCOUNT]
        ([BankAccountID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TRANSACTI__BankA__5165187F'
CREATE INDEX [IX_FK__TRANSACTI__BankA__5165187F]
ON [dbo].[TRANSACTION_DETAIL]
    ([BankAccountID]);
GO

-- Creating foreign key on [BookCollectionID] in table 'BOOK_EDITION'
ALTER TABLE [dbo].[BOOK_EDITION]
ADD CONSTRAINT [FK_BookCollectionID]
    FOREIGN KEY ([BookCollectionID])
    REFERENCES [dbo].[BOOK_COLLECTION]
        ([BookCollectionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookCollectionID'
CREATE INDEX [IX_FK_BookCollectionID]
ON [dbo].[BOOK_EDITION]
    ([BookCollectionID]);
GO

-- Creating foreign key on [EditionID] in table 'BOOK_EDITION_IMAGE'
ALTER TABLE [dbo].[BOOK_EDITION_IMAGE]
ADD CONSTRAINT [FK__BOOK_EDIT__Editi__123EB7A3]
    FOREIGN KEY ([EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOK_EDIT__Editi__123EB7A3'
CREATE INDEX [IX_FK__BOOK_EDIT__Editi__123EB7A3]
ON [dbo].[BOOK_EDITION_IMAGE]
    ([EditionID]);
GO

-- Creating foreign key on [EditionID] in table 'BOOK_REVIEW'
ALTER TABLE [dbo].[BOOK_REVIEW]
ADD CONSTRAINT [FK__BOOK_REVI__Editi__0E6E26BF]
    FOREIGN KEY ([EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOK_REVI__Editi__0E6E26BF'
CREATE INDEX [IX_FK__BOOK_REVI__Editi__0E6E26BF]
ON [dbo].[BOOK_REVIEW]
    ([EditionID]);
GO

-- Creating foreign key on [EditionID] in table 'CUSTOMER_ORDER_DETAIL'
ALTER TABLE [dbo].[CUSTOMER_ORDER_DETAIL]
ADD CONSTRAINT [FK__CUSTOMER___Editi__236943A5]
    FOREIGN KEY ([EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Editi__236943A5'
CREATE INDEX [IX_FK__CUSTOMER___Editi__236943A5]
ON [dbo].[CUSTOMER_ORDER_DETAIL]
    ([EditionID]);
GO

-- Creating foreign key on [EditionID] in table 'STOCK_INVENTORY'
ALTER TABLE [dbo].[STOCK_INVENTORY]
ADD CONSTRAINT [FK__STOCK_INV__Editi__09A971A2]
    FOREIGN KEY ([EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EditionID] in table 'STOCK_RECEIVED_NOTE_DETAIL'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL]
ADD CONSTRAINT [FK__STOCK_REC__Editi__01142BA1]
    FOREIGN KEY ([EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CustomerID] in table 'BOOK_REVIEW'
ALTER TABLE [dbo].[BOOK_REVIEW]
ADD CONSTRAINT [FK__BOOK_REVI__Custo__0F624AF8]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[CUSTOMERs]
        ([CustomerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOK_REVI__Custo__0F624AF8'
CREATE INDEX [IX_FK__BOOK_REVI__Custo__0F624AF8]
ON [dbo].[BOOK_REVIEW]
    ([CustomerID]);
GO

-- Creating foreign key on [CustomerID] in table 'CUSTOMER_ORDER'
ALTER TABLE [dbo].[CUSTOMER_ORDER]
ADD CONSTRAINT [FK__CUSTOMER___Custo__1DB06A4F]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[CUSTOMERs]
        ([CustomerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Custo__1DB06A4F'
CREATE INDEX [IX_FK__CUSTOMER___Custo__1DB06A4F]
ON [dbo].[CUSTOMER_ORDER]
    ([CustomerID]);
GO

-- Creating foreign key on [TierID] in table 'CUSTOMERs'
ALTER TABLE [dbo].[CUSTOMERs]
ADD CONSTRAINT [FK__CUSTOMER__TierID__4316F928]
    FOREIGN KEY ([TierID])
    REFERENCES [dbo].[TIERs]
        ([TierID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER__TierID__4316F928'
CREATE INDEX [IX_FK__CUSTOMER__TierID__4316F928]
ON [dbo].[CUSTOMERs]
    ([TierID]);
GO

-- Creating foreign key on [CustomerID] in table 'WALLETs'
ALTER TABLE [dbo].[WALLETs]
ADD CONSTRAINT [FK__WALLET__Customer__47DBAE45]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[CUSTOMERs]
        ([CustomerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__WALLET__Customer__47DBAE45'
CREATE INDEX [IX_FK__WALLET__Customer__47DBAE45]
ON [dbo].[WALLETs]
    ([CustomerID]);
GO

-- Creating foreign key on [OrderID] in table 'CUSTOMER_ORDER_DETAIL'
ALTER TABLE [dbo].[CUSTOMER_ORDER_DETAIL]
ADD CONSTRAINT [FK__CUSTOMER___Order__245D67DE]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[CUSTOMER_ORDER]
        ([OrderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ShipperID] in table 'CUSTOMER_ORDER'
ALTER TABLE [dbo].[CUSTOMER_ORDER]
ADD CONSTRAINT [FK__CUSTOMER___Shipp__1EA48E88]
    FOREIGN KEY ([ShipperID])
    REFERENCES [dbo].[SHIPPERs]
        ([ShipperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Shipp__1EA48E88'
CREATE INDEX [IX_FK__CUSTOMER___Shipp__1EA48E88]
ON [dbo].[CUSTOMER_ORDER]
    ([ShipperID]);
GO

-- Creating foreign key on [StaffID] in table 'CUSTOMER_ORDER'
ALTER TABLE [dbo].[CUSTOMER_ORDER]
ADD CONSTRAINT [FK__CUSTOMER___Staff__1CBC4616]
    FOREIGN KEY ([StaffID])
    REFERENCES [dbo].[STAFFs]
        ([StaffID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Staff__1CBC4616'
CREATE INDEX [IX_FK__CUSTOMER___Staff__1CBC4616]
ON [dbo].[CUSTOMER_ORDER]
    ([StaffID]);
GO

-- Creating foreign key on [OrderID] in table 'SHIP_CONFIRMATION'
ALTER TABLE [dbo].[SHIP_CONFIRMATION]
ADD CONSTRAINT [FK__SHIP_CONF__Order__282DF8C2]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[CUSTOMER_ORDER]
        ([OrderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__SHIP_CONF__Order__282DF8C2'
CREATE INDEX [IX_FK__SHIP_CONF__Order__282DF8C2]
ON [dbo].[SHIP_CONFIRMATION]
    ([OrderID]);
GO

-- Creating foreign key on [ManagerID] in table 'STOCK_RECEIVED_NOTE'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE]
ADD CONSTRAINT [FK__STOCK_REC__Manag__7B5B524B]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[MANAGERs]
        ([ManagerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__STOCK_REC__Manag__7B5B524B'
CREATE INDEX [IX_FK__STOCK_REC__Manag__7B5B524B]
ON [dbo].[STOCK_RECEIVED_NOTE]
    ([ManagerID]);
GO

-- Creating foreign key on [PublisherID] in table 'STOCK_RECEIVED_NOTE'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE]
ADD CONSTRAINT [FK__STOCK_REC__Publi__7A672E12]
    FOREIGN KEY ([PublisherID])
    REFERENCES [dbo].[PUBLISHERs]
        ([PublisherID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__STOCK_REC__Publi__7A672E12'
CREATE INDEX [IX_FK__STOCK_REC__Publi__7A672E12]
ON [dbo].[STOCK_RECEIVED_NOTE]
    ([PublisherID]);
GO

-- Creating foreign key on [ShipperID] in table 'SHIP_CONFIRMATION'
ALTER TABLE [dbo].[SHIP_CONFIRMATION]
ADD CONSTRAINT [FK__SHIP_CONF__Shipp__29221CFB]
    FOREIGN KEY ([ShipperID])
    REFERENCES [dbo].[SHIPPERs]
        ([ShipperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__SHIP_CONF__Shipp__29221CFB'
CREATE INDEX [IX_FK__SHIP_CONF__Shipp__29221CFB]
ON [dbo].[SHIP_CONFIRMATION]
    ([ShipperID]);
GO

-- Creating foreign key on [StockReceivedNoteID] in table 'STOCK_RECEIVED_NOTE_DETAIL'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL]
ADD CONSTRAINT [FK__STOCK_REC__Stock__02084FDA]
    FOREIGN KEY ([StockReceivedNoteID])
    REFERENCES [dbo].[STOCK_RECEIVED_NOTE]
        ([StockReceivedNoteID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__STOCK_REC__Stock__02084FDA'
CREATE INDEX [IX_FK__STOCK_REC__Stock__02084FDA]
ON [dbo].[STOCK_RECEIVED_NOTE_DETAIL]
    ([StockReceivedNoteID]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [CATEGORies_CategoryID] in table 'BOOK_CATEGORY'
ALTER TABLE [dbo].[BOOK_CATEGORY]
ADD CONSTRAINT [FK_BOOK_CATEGORY_CATEGORY]
    FOREIGN KEY ([CATEGORies_CategoryID])
    REFERENCES [dbo].[CATEGORies]
        ([CategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BOOK_EDITION_EditionID] in table 'BOOK_CATEGORY'
ALTER TABLE [dbo].[BOOK_CATEGORY]
ADD CONSTRAINT [FK_BOOK_CATEGORY_BOOK_EDITION]
    FOREIGN KEY ([BOOK_EDITION_EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BOOK_CATEGORY_BOOK_EDITION'
CREATE INDEX [IX_FK_BOOK_CATEGORY_BOOK_EDITION]
ON [dbo].[BOOK_CATEGORY]
    ([BOOK_EDITION_EditionID]);
GO

-- Creating foreign key on [BOOK_EDITION_EditionID] in table 'BOOK_PROMOTION'
ALTER TABLE [dbo].[BOOK_PROMOTION]
ADD CONSTRAINT [FK_BOOK_PROMOTION_BOOK_EDITION]
    FOREIGN KEY ([BOOK_EDITION_EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PROMOTIONs_PromotionID] in table 'BOOK_PROMOTION'
ALTER TABLE [dbo].[BOOK_PROMOTION]
ADD CONSTRAINT [FK_BOOK_PROMOTION_PROMOTION]
    FOREIGN KEY ([PROMOTIONs_PromotionID])
    REFERENCES [dbo].[PROMOTIONs]
        ([PromotionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BOOK_PROMOTION_PROMOTION'
CREATE INDEX [IX_FK_BOOK_PROMOTION_PROMOTION]
ON [dbo].[BOOK_PROMOTION]
    ([PROMOTIONs_PromotionID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------