
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/22/2023 20:23:50
-- Generated from EDMX file: C:\Users\ADMIN\OneDrive\Máy tính\folder\btl\New folder (6)\Book_Store_CSharp\WebApplication2\Models\BookStoreManager.edmx
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

IF OBJECT_ID(N'[dbo].[FK__BANK_ACCO__Custo__5EBF139D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BANK_ACCOUNT] DROP CONSTRAINT [FK__BANK_ACCO__Custo__5EBF139D];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_CATE__Categ__7C4F7684]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_CATEGORY] DROP CONSTRAINT [FK__BOOK_CATE__Categ__7C4F7684];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_CATE__Editi__7B5B524B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_CATEGORY] DROP CONSTRAINT [FK__BOOK_CATE__Editi__7B5B524B];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_COLL__Manag__6EF57B66]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_COLLECTION] DROP CONSTRAINT [FK__BOOK_COLL__Manag__6EF57B66];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_EDIT__BookC__7D439ABD]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_EDITION] DROP CONSTRAINT [FK__BOOK_EDIT__BookC__7D439ABD];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_EDIT__Editi__1EA48E88]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_EDITION_IMAGE] DROP CONSTRAINT [FK__BOOK_EDIT__Editi__1EA48E88];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_EDIT__Manag__74AE54BC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_EDITION] DROP CONSTRAINT [FK__BOOK_EDIT__Manag__74AE54BC];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_PROM__Editi__01142BA1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_PROMOTION] DROP CONSTRAINT [FK__BOOK_PROM__Editi__01142BA1];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_PROM__Promo__00200768]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_PROMOTION] DROP CONSTRAINT [FK__BOOK_PROM__Promo__00200768];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_REVI__Custo__1AD3FDA4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_REVIEW] DROP CONSTRAINT [FK__BOOK_REVI__Custo__1AD3FDA4];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOK_REVI__Editi__19DFD96B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_REVIEW] DROP CONSTRAINT [FK__BOOK_REVI__Editi__19DFD96B];
GO
IF OBJECT_ID(N'[dbo].[FK__CATEGORY__Manage__66603565]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CATEGORY] DROP CONSTRAINT [FK__CATEGORY__Manage__66603565];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Custo__12FDD1B2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER] DROP CONSTRAINT [FK__CUSTOMER___Custo__12FDD1B2];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Editi__1D7B6025]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER_DETAIL] DROP CONSTRAINT [FK__CUSTOMER___Editi__1D7B6025];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Order__16CE6296]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER_STATUS] DROP CONSTRAINT [FK__CUSTOMER___Order__16CE6296];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Order__1E6F845E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER_DETAIL] DROP CONSTRAINT [FK__CUSTOMER___Order__1E6F845E];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Shipp__13F1F5EB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER] DROP CONSTRAINT [FK__CUSTOMER___Shipp__13F1F5EB];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Staff__1209AD79]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER] DROP CONSTRAINT [FK__CUSTOMER___Staff__1209AD79];
GO
IF OBJECT_ID(N'[dbo].[FK__CUSTOMER___Statu__17C286CF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER_ORDER_STATUS] DROP CONSTRAINT [FK__CUSTOMER___Statu__17C286CF];
GO
IF OBJECT_ID(N'[dbo].[FK__MARKED_BO__Custo__778AC167]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MARKED_BOOK] DROP CONSTRAINT [FK__MARKED_BO__Custo__778AC167];
GO
IF OBJECT_ID(N'[dbo].[FK__MARKED_BO__Editi__787EE5A0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MARKED_BOOK] DROP CONSTRAINT [FK__MARKED_BO__Editi__787EE5A0];
GO
IF OBJECT_ID(N'[dbo].[FK__Person__ManagerI__5535A963]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK__Person__ManagerI__5535A963];
GO
IF OBJECT_ID(N'[dbo].[FK__Person__TierID__5441852A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK__Person__TierID__5441852A];
GO
IF OBJECT_ID(N'[dbo].[FK__PROMOTION__Manag__6C190EBB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PROMOTION] DROP CONSTRAINT [FK__PROMOTION__Manag__6C190EBB];
GO
IF OBJECT_ID(N'[dbo].[FK__PUBLISHER__Manag__628FA481]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PUBLISHER] DROP CONSTRAINT [FK__PUBLISHER__Manag__628FA481];
GO
IF OBJECT_ID(N'[dbo].[FK__SHIP_CONF__Order__2AD55B43]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SHIP_CONFIRMATION] DROP CONSTRAINT [FK__SHIP_CONF__Order__2AD55B43];
GO
IF OBJECT_ID(N'[dbo].[FK__SHIP_CONF__Shipp__2BC97F7C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SHIP_CONFIRMATION] DROP CONSTRAINT [FK__SHIP_CONF__Shipp__2BC97F7C];
GO
IF OBJECT_ID(N'[dbo].[FK__STOCK_INV__Editi__151B244E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STOCK_INVENTORY] DROP CONSTRAINT [FK__STOCK_INV__Editi__151B244E];
GO
IF OBJECT_ID(N'[dbo].[FK__STOCK_REC__Editi__0C85DE4D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL] DROP CONSTRAINT [FK__STOCK_REC__Editi__0C85DE4D];
GO
IF OBJECT_ID(N'[dbo].[FK__STOCK_REC__Manag__05D8E0BE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE] DROP CONSTRAINT [FK__STOCK_REC__Manag__05D8E0BE];
GO
IF OBJECT_ID(N'[dbo].[FK__STOCK_REC__Publi__04E4BC85]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE] DROP CONSTRAINT [FK__STOCK_REC__Publi__04E4BC85];
GO
IF OBJECT_ID(N'[dbo].[FK__STOCK_REC__Stock__0D7A0286]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL] DROP CONSTRAINT [FK__STOCK_REC__Stock__0D7A0286];
GO
IF OBJECT_ID(N'[dbo].[FK__TIER__ManagerID__4E88ABD4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TIER] DROP CONSTRAINT [FK__TIER__ManagerID__4E88ABD4];
GO
IF OBJECT_ID(N'[dbo].[FK__TRANSACTI__BankA__24285DB4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TRANSACTION_DETAILS] DROP CONSTRAINT [FK__TRANSACTI__BankA__24285DB4];
GO
IF OBJECT_ID(N'[dbo].[FK__TRANSACTI__Order__251C81ED]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TRANSACTION_DETAILS] DROP CONSTRAINT [FK__TRANSACTI__Order__251C81ED];
GO
IF OBJECT_ID(N'[dbo].[FK__TRANSACTI__Walle__2334397B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TRANSACTION_DETAILS] DROP CONSTRAINT [FK__TRANSACTI__Walle__2334397B];
GO
IF OBJECT_ID(N'[dbo].[FK__WALLET__Customer__59FA5E80]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WALLET] DROP CONSTRAINT [FK__WALLET__Customer__59FA5E80];
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
IF OBJECT_ID(N'[dbo].[FK_MANAGER_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MANAGER] DROP CONSTRAINT [FK_MANAGER_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Person_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK_Person_AspNetUsers];
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
IF OBJECT_ID(N'[dbo].[CUSTOMER_ORDER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CUSTOMER_ORDER];
GO
IF OBJECT_ID(N'[dbo].[CUSTOMER_ORDER_DETAIL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CUSTOMER_ORDER_DETAIL];
GO
IF OBJECT_ID(N'[dbo].[CUSTOMER_ORDER_STATUS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CUSTOMER_ORDER_STATUS];
GO
IF OBJECT_ID(N'[dbo].[MANAGER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MANAGER];
GO
IF OBJECT_ID(N'[dbo].[MARKED_BOOK]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MARKED_BOOK];
GO
IF OBJECT_ID(N'[dbo].[ORDER_STATUS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ORDER_STATUS];
GO
IF OBJECT_ID(N'[dbo].[Person]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Person];
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
IF OBJECT_ID(N'[dbo].[TRANSACTION_DETAILS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TRANSACTION_DETAILS];
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
    [BankCVC] nvarchar(3)  NOT NULL,
    [CustomerID] int  NOT NULL
);
GO

-- Creating table 'BOOK_COLLECTION'
CREATE TABLE [dbo].[BOOK_COLLECTION] (
    [BookCollectionID] int IDENTITY(1,1) NOT NULL,
    [BookCollectionName] nvarchar(200)  NOT NULL,
    [ManagerID] int  NOT NULL
);
GO

-- Creating table 'BOOK_EDITION'
CREATE TABLE [dbo].[BOOK_EDITION] (
    [EditionID] int IDENTITY(1,1) NOT NULL,
    [EditionPrice] decimal(12,2)  NOT NULL,
    [EditionDescription] nvarchar(500)  NULL,
    [EditionYear] nvarchar(4)  NOT NULL,
    [EditionPageCount] int  NULL,
    [EditionName] nvarchar(200)  NOT NULL,
    [EditionAuthor] nvarchar(100)  NOT NULL,
    [ManagerID] int  NOT NULL,
    [BookCollectionID] int  NULL
);
GO

-- Creating table 'BOOK_EDITION_IMAGE'
CREATE TABLE [dbo].[BOOK_EDITION_IMAGE] (
    [EditionImageID] int IDENTITY(1,1) NOT NULL,
    [EditionImage] nvarchar(100)  NOT NULL,
    [EditionID] int  NOT NULL
);
GO

-- Creating table 'BOOK_REVIEW'
CREATE TABLE [dbo].[BOOK_REVIEW] (
    [ReviewID] int IDENTITY(1,1) NOT NULL,
    [ReviewDescription] nvarchar(1000)  NOT NULL,
    [ReviewDate] datetime  NOT NULL,
    [CustomerID] int  NOT NULL,
    [EditionID] int  NOT NULL,
    [ReviewRating] int  NOT NULL
);
GO

-- Creating table 'CATEGORies'
CREATE TABLE [dbo].[CATEGORies] (
    [CategoryID] int IDENTITY(1,1) NOT NULL,
    [CategoryName] nvarchar(100)  NOT NULL,
    [CategoryDescription] nvarchar(100)  NOT NULL,
    [ManagerID] int  NOT NULL
);
GO

-- Creating table 'CUSTOMER_ORDER'
CREATE TABLE [dbo].[CUSTOMER_ORDER] (
    [OrderID] int IDENTITY(1,1) NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [OrderTotalPrice] decimal(12,2)  NOT NULL,
    [OrderShippingMethod] nvarchar(50)  NOT NULL,
    [OrderPaymentMethod] nvarchar(50)  NOT NULL,
    [StaffID] int  NULL,
    [CustomerID] int  NOT NULL,
    [ShipperID] int  NULL
);
GO

-- Creating table 'CUSTOMER_ORDER_DETAIL'
CREATE TABLE [dbo].[CUSTOMER_ORDER_DETAIL] (
    [OrderDetailID] int IDENTITY(1,1) NOT NULL,
    [DetailCurrentPrice] decimal(12,2)  NULL,
    [DetailQuantity] int  NOT NULL,
    [OrderID] int  NOT NULL,
    [EditionID] int  NOT NULL
);
GO

-- Creating table 'MANAGERs'
CREATE TABLE [dbo].[MANAGERs] (
    [ManagerID] int IDENTITY(1,1) NOT NULL,
    [AccountID] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [PersonID] int IDENTITY(1,1) NOT NULL,
    [PersonName] nvarchar(50)  NOT NULL,
    [PersonAddress] nvarchar(100)  NULL,
    [PersonStatus] nvarchar(50)  NULL,
    [AccountID] nvarchar(128)  NULL,
    [TierID] int  NULL,
    [ManagerID] int  NOT NULL
);
GO

-- Creating table 'PROMOTIONs'
CREATE TABLE [dbo].[PROMOTIONs] (
    [PromotionID] int IDENTITY(1,1) NOT NULL,
    [PromotionName] nvarchar(200)  NOT NULL,
    [PromotionDiscount] decimal(4,2)  NOT NULL,
    [PromotionStartDate] datetime  NOT NULL,
    [PromotionEndDate] datetime  NOT NULL,
    [ManagerID] int  NOT NULL,
    [PromotionDetails] varchar(max)  NULL,
    [PromotionIsValid] bit  NOT NULL
);
GO

-- Creating table 'PUBLISHERs'
CREATE TABLE [dbo].[PUBLISHERs] (
    [PublisherID] int IDENTITY(1,1) NOT NULL,
    [PublisherName] nvarchar(100)  NOT NULL,
    [PublisherDescription] nvarchar(500)  NULL,
    [PublisherImage] nvarchar(200)  NULL,
    [ManagerID] int  NOT NULL
);
GO

-- Creating table 'SHIP_CONFIRMATION'
CREATE TABLE [dbo].[SHIP_CONFIRMATION] (
    [ConfirmationID] int IDENTITY(1,1) NOT NULL,
    [ConfirmationDate] datetime  NOT NULL,
    [ConfirmationImage] nvarchar(50)  NOT NULL,
    [OrderID] int  NOT NULL,
    [ShipperID] int  NOT NULL
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
    [PublisherID] int  NOT NULL,
    [ManagerID] int  NOT NULL
);
GO

-- Creating table 'STOCK_RECEIVED_NOTE_DETAIL'
CREATE TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL] (
    [NoteDetailID] int IDENTITY(1,1) NOT NULL,
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
    [TierLevel] int  NOT NULL,
    [ManagerID] int  NOT NULL
);
GO

-- Creating table 'TRANSACTION_DETAILS'
CREATE TABLE [dbo].[TRANSACTION_DETAILS] (
    [TransactionID] int IDENTITY(1,1) NOT NULL,
    [TransactionDate] datetime  NOT NULL,
    [TransactionType] nvarchar(128)  NOT NULL,
    [TransactionAmount] decimal(14,2)  NULL,
    [WalletID] int  NULL,
    [BankAccountID] int  NULL,
    [OrderID] int  NULL
);
GO

-- Creating table 'WALLETs'
CREATE TABLE [dbo].[WALLETs] (
    [WalletID] int IDENTITY(1,1) NOT NULL,
    [CustomerID] int  NOT NULL,
    [Balance] decimal(12,2)  NOT NULL
);
GO

-- Creating table 'V_UserRole'
CREATE TABLE [dbo].[V_UserRole] (
    [Name] nvarchar(256)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [PersonID] int  NOT NULL,
    [Id] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'CUSTOMER_ORDER_STATUS'
CREATE TABLE [dbo].[CUSTOMER_ORDER_STATUS] (
    [OrderStatusID] int IDENTITY(1,1) NOT NULL,
    [OrderID] int  NOT NULL,
    [StatusID] int  NOT NULL,
    [UpdateTime] datetime  NOT NULL
);
GO

-- Creating table 'ORDER_STATUS'
CREATE TABLE [dbo].[ORDER_STATUS] (
    [StatusID] int IDENTITY(1,1) NOT NULL,
    [OrderStatus] nvarchar(50)  NOT NULL
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

-- Creating table 'MARKED_BOOK'
CREATE TABLE [dbo].[MARKED_BOOK] (
    [People_PersonID] int  NOT NULL,
    [BOOK_EDITION_EditionID] int  NOT NULL
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

-- Creating primary key on [OrderID] in table 'CUSTOMER_ORDER'
ALTER TABLE [dbo].[CUSTOMER_ORDER]
ADD CONSTRAINT [PK_CUSTOMER_ORDER]
    PRIMARY KEY CLUSTERED ([OrderID] ASC);
GO

-- Creating primary key on [OrderDetailID] in table 'CUSTOMER_ORDER_DETAIL'
ALTER TABLE [dbo].[CUSTOMER_ORDER_DETAIL]
ADD CONSTRAINT [PK_CUSTOMER_ORDER_DETAIL]
    PRIMARY KEY CLUSTERED ([OrderDetailID] ASC);
GO

-- Creating primary key on [ManagerID] in table 'MANAGERs'
ALTER TABLE [dbo].[MANAGERs]
ADD CONSTRAINT [PK_MANAGERs]
    PRIMARY KEY CLUSTERED ([ManagerID] ASC);
GO

-- Creating primary key on [PersonID] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([PersonID] ASC);
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

-- Creating primary key on [ConfirmationID] in table 'SHIP_CONFIRMATION'
ALTER TABLE [dbo].[SHIP_CONFIRMATION]
ADD CONSTRAINT [PK_SHIP_CONFIRMATION]
    PRIMARY KEY CLUSTERED ([ConfirmationID] ASC);
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

-- Creating primary key on [NoteDetailID] in table 'STOCK_RECEIVED_NOTE_DETAIL'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL]
ADD CONSTRAINT [PK_STOCK_RECEIVED_NOTE_DETAIL]
    PRIMARY KEY CLUSTERED ([NoteDetailID] ASC);
GO

-- Creating primary key on [TierID] in table 'TIERs'
ALTER TABLE [dbo].[TIERs]
ADD CONSTRAINT [PK_TIERs]
    PRIMARY KEY CLUSTERED ([TierID] ASC);
GO

-- Creating primary key on [TransactionID] in table 'TRANSACTION_DETAILS'
ALTER TABLE [dbo].[TRANSACTION_DETAILS]
ADD CONSTRAINT [PK_TRANSACTION_DETAILS]
    PRIMARY KEY CLUSTERED ([TransactionID] ASC);
GO

-- Creating primary key on [WalletID] in table 'WALLETs'
ALTER TABLE [dbo].[WALLETs]
ADD CONSTRAINT [PK_WALLETs]
    PRIMARY KEY CLUSTERED ([WalletID] ASC);
GO

-- Creating primary key on [Name], [UserId], [PersonID], [Id] in table 'V_UserRole'
ALTER TABLE [dbo].[V_UserRole]
ADD CONSTRAINT [PK_V_UserRole]
    PRIMARY KEY CLUSTERED ([Name], [UserId], [PersonID], [Id] ASC);
GO

-- Creating primary key on [OrderStatusID] in table 'CUSTOMER_ORDER_STATUS'
ALTER TABLE [dbo].[CUSTOMER_ORDER_STATUS]
ADD CONSTRAINT [PK_CUSTOMER_ORDER_STATUS]
    PRIMARY KEY CLUSTERED ([OrderStatusID] ASC);
GO

-- Creating primary key on [StatusID] in table 'ORDER_STATUS'
ALTER TABLE [dbo].[ORDER_STATUS]
ADD CONSTRAINT [PK_ORDER_STATUS]
    PRIMARY KEY CLUSTERED ([StatusID] ASC);
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

-- Creating primary key on [People_PersonID], [BOOK_EDITION_EditionID] in table 'MARKED_BOOK'
ALTER TABLE [dbo].[MARKED_BOOK]
ADD CONSTRAINT [PK_MARKED_BOOK]
    PRIMARY KEY CLUSTERED ([People_PersonID], [BOOK_EDITION_EditionID] ASC);
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

-- Creating foreign key on [AccountID] in table 'MANAGERs'
ALTER TABLE [dbo].[MANAGERs]
ADD CONSTRAINT [FK_MANAGER_AspNetUsers]
    FOREIGN KEY ([AccountID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MANAGER_AspNetUsers'
CREATE INDEX [IX_FK_MANAGER_AspNetUsers]
ON [dbo].[MANAGERs]
    ([AccountID]);
GO

-- Creating foreign key on [AccountID] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [FK_Person_AspNetUsers]
    FOREIGN KEY ([AccountID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Person_AspNetUsers'
CREATE INDEX [IX_FK_Person_AspNetUsers]
ON [dbo].[People]
    ([AccountID]);
GO

-- Creating foreign key on [CustomerID] in table 'BANK_ACCOUNT'
ALTER TABLE [dbo].[BANK_ACCOUNT]
ADD CONSTRAINT [FK__BANK_ACCO__Custo__4F7CD00D]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[People]
        ([PersonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BANK_ACCO__Custo__4F7CD00D'
CREATE INDEX [IX_FK__BANK_ACCO__Custo__4F7CD00D]
ON [dbo].[BANK_ACCOUNT]
    ([CustomerID]);
GO

-- Creating foreign key on [BankAccountID] in table 'TRANSACTION_DETAILS'
ALTER TABLE [dbo].[TRANSACTION_DETAILS]
ADD CONSTRAINT [FK__TRANSACTI__BankA__2739D489]
    FOREIGN KEY ([BankAccountID])
    REFERENCES [dbo].[BANK_ACCOUNT]
        ([BankAccountID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TRANSACTI__BankA__2739D489'
CREATE INDEX [IX_FK__TRANSACTI__BankA__2739D489]
ON [dbo].[TRANSACTION_DETAILS]
    ([BankAccountID]);
GO

-- Creating foreign key on [ManagerID] in table 'BOOK_COLLECTION'
ALTER TABLE [dbo].[BOOK_COLLECTION]
ADD CONSTRAINT [FK__BOOK_COLL__Manag__5FB337D6]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[MANAGERs]
        ([ManagerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOK_COLL__Manag__5FB337D6'
CREATE INDEX [IX_FK__BOOK_COLL__Manag__5FB337D6]
ON [dbo].[BOOK_COLLECTION]
    ([ManagerID]);
GO

-- Creating foreign key on [BookCollectionID] in table 'BOOK_EDITION'
ALTER TABLE [dbo].[BOOK_EDITION]
ADD CONSTRAINT [FK__BOOK_EDIT__BookC__6E01572D]
    FOREIGN KEY ([BookCollectionID])
    REFERENCES [dbo].[BOOK_COLLECTION]
        ([BookCollectionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOK_EDIT__BookC__6E01572D'
CREATE INDEX [IX_FK__BOOK_EDIT__BookC__6E01572D]
ON [dbo].[BOOK_EDITION]
    ([BookCollectionID]);
GO

-- Creating foreign key on [EditionID] in table 'BOOK_EDITION_IMAGE'
ALTER TABLE [dbo].[BOOK_EDITION_IMAGE]
ADD CONSTRAINT [FK__BOOK_EDIT__Editi__0F624AF8]
    FOREIGN KEY ([EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOK_EDIT__Editi__0F624AF8'
CREATE INDEX [IX_FK__BOOK_EDIT__Editi__0F624AF8]
ON [dbo].[BOOK_EDITION_IMAGE]
    ([EditionID]);
GO

-- Creating foreign key on [ManagerID] in table 'BOOK_EDITION'
ALTER TABLE [dbo].[BOOK_EDITION]
ADD CONSTRAINT [FK__BOOK_EDIT__Manag__656C112C]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[MANAGERs]
        ([ManagerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOK_EDIT__Manag__656C112C'
CREATE INDEX [IX_FK__BOOK_EDIT__Manag__656C112C]
ON [dbo].[BOOK_EDITION]
    ([ManagerID]);
GO

-- Creating foreign key on [EditionID] in table 'BOOK_REVIEW'
ALTER TABLE [dbo].[BOOK_REVIEW]
ADD CONSTRAINT [FK__BOOK_REVI__Editi__0A9D95DB]
    FOREIGN KEY ([EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOK_REVI__Editi__0A9D95DB'
CREATE INDEX [IX_FK__BOOK_REVI__Editi__0A9D95DB]
ON [dbo].[BOOK_REVIEW]
    ([EditionID]);
GO

-- Creating foreign key on [EditionID] in table 'CUSTOMER_ORDER_DETAIL'
ALTER TABLE [dbo].[CUSTOMER_ORDER_DETAIL]
ADD CONSTRAINT [FK__CUSTOMER___Editi__208CD6FA]
    FOREIGN KEY ([EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Editi__208CD6FA'
CREATE INDEX [IX_FK__CUSTOMER___Editi__208CD6FA]
ON [dbo].[CUSTOMER_ORDER_DETAIL]
    ([EditionID]);
GO

-- Creating foreign key on [EditionID] in table 'STOCK_INVENTORY'
ALTER TABLE [dbo].[STOCK_INVENTORY]
ADD CONSTRAINT [FK__STOCK_INV__Editi__05D8E0BE]
    FOREIGN KEY ([EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EditionID] in table 'STOCK_RECEIVED_NOTE_DETAIL'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL]
ADD CONSTRAINT [FK__STOCK_REC__Editi__7D439ABD]
    FOREIGN KEY ([EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__STOCK_REC__Editi__7D439ABD'
CREATE INDEX [IX_FK__STOCK_REC__Editi__7D439ABD]
ON [dbo].[STOCK_RECEIVED_NOTE_DETAIL]
    ([EditionID]);
GO

-- Creating foreign key on [CustomerID] in table 'BOOK_REVIEW'
ALTER TABLE [dbo].[BOOK_REVIEW]
ADD CONSTRAINT [FK__BOOK_REVI__Custo__0B91BA14]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[People]
        ([PersonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOK_REVI__Custo__0B91BA14'
CREATE INDEX [IX_FK__BOOK_REVI__Custo__0B91BA14]
ON [dbo].[BOOK_REVIEW]
    ([CustomerID]);
GO

-- Creating foreign key on [ManagerID] in table 'CATEGORies'
ALTER TABLE [dbo].[CATEGORies]
ADD CONSTRAINT [FK__CATEGORY__Manage__571DF1D5]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[MANAGERs]
        ([ManagerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CATEGORY__Manage__571DF1D5'
CREATE INDEX [IX_FK__CATEGORY__Manage__571DF1D5]
ON [dbo].[CATEGORies]
    ([ManagerID]);
GO

-- Creating foreign key on [CustomerID] in table 'CUSTOMER_ORDER'
ALTER TABLE [dbo].[CUSTOMER_ORDER]
ADD CONSTRAINT [FK__CUSTOMER___Custo__1AD3FDA4]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[People]
        ([PersonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Custo__1AD3FDA4'
CREATE INDEX [IX_FK__CUSTOMER___Custo__1AD3FDA4]
ON [dbo].[CUSTOMER_ORDER]
    ([CustomerID]);
GO

-- Creating foreign key on [OrderID] in table 'CUSTOMER_ORDER_DETAIL'
ALTER TABLE [dbo].[CUSTOMER_ORDER_DETAIL]
ADD CONSTRAINT [FK__CUSTOMER___Order__2180FB33]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[CUSTOMER_ORDER]
        ([OrderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Order__2180FB33'
CREATE INDEX [IX_FK__CUSTOMER___Order__2180FB33]
ON [dbo].[CUSTOMER_ORDER_DETAIL]
    ([OrderID]);
GO

-- Creating foreign key on [ShipperID] in table 'CUSTOMER_ORDER'
ALTER TABLE [dbo].[CUSTOMER_ORDER]
ADD CONSTRAINT [FK__CUSTOMER___Shipp__1BC821DD]
    FOREIGN KEY ([ShipperID])
    REFERENCES [dbo].[People]
        ([PersonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Shipp__1BC821DD'
CREATE INDEX [IX_FK__CUSTOMER___Shipp__1BC821DD]
ON [dbo].[CUSTOMER_ORDER]
    ([ShipperID]);
GO

-- Creating foreign key on [StaffID] in table 'CUSTOMER_ORDER'
ALTER TABLE [dbo].[CUSTOMER_ORDER]
ADD CONSTRAINT [FK__CUSTOMER___Staff__19DFD96B]
    FOREIGN KEY ([StaffID])
    REFERENCES [dbo].[People]
        ([PersonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Staff__19DFD96B'
CREATE INDEX [IX_FK__CUSTOMER___Staff__19DFD96B]
ON [dbo].[CUSTOMER_ORDER]
    ([StaffID]);
GO

-- Creating foreign key on [OrderID] in table 'SHIP_CONFIRMATION'
ALTER TABLE [dbo].[SHIP_CONFIRMATION]
ADD CONSTRAINT [FK__SHIP_CONF__Order__2DE6D218]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[CUSTOMER_ORDER]
        ([OrderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__SHIP_CONF__Order__2DE6D218'
CREATE INDEX [IX_FK__SHIP_CONF__Order__2DE6D218]
ON [dbo].[SHIP_CONFIRMATION]
    ([OrderID]);
GO

-- Creating foreign key on [OrderID] in table 'TRANSACTION_DETAILS'
ALTER TABLE [dbo].[TRANSACTION_DETAILS]
ADD CONSTRAINT [FK__TRANSACTI__Order__282DF8C2]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[CUSTOMER_ORDER]
        ([OrderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TRANSACTI__Order__282DF8C2'
CREATE INDEX [IX_FK__TRANSACTI__Order__282DF8C2]
ON [dbo].[TRANSACTION_DETAILS]
    ([OrderID]);
GO

-- Creating foreign key on [ManagerID] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [FK__Person__ManagerI__45F365D3]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[MANAGERs]
        ([ManagerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Person__ManagerI__45F365D3'
CREATE INDEX [IX_FK__Person__ManagerI__45F365D3]
ON [dbo].[People]
    ([ManagerID]);
GO

-- Creating foreign key on [ManagerID] in table 'PROMOTIONs'
ALTER TABLE [dbo].[PROMOTIONs]
ADD CONSTRAINT [FK__PROMOTION__Manag__5CD6CB2B]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[MANAGERs]
        ([ManagerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__PROMOTION__Manag__5CD6CB2B'
CREATE INDEX [IX_FK__PROMOTION__Manag__5CD6CB2B]
ON [dbo].[PROMOTIONs]
    ([ManagerID]);
GO

-- Creating foreign key on [ManagerID] in table 'PUBLISHERs'
ALTER TABLE [dbo].[PUBLISHERs]
ADD CONSTRAINT [FK__PUBLISHER__Manag__534D60F1]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[MANAGERs]
        ([ManagerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__PUBLISHER__Manag__534D60F1'
CREATE INDEX [IX_FK__PUBLISHER__Manag__534D60F1]
ON [dbo].[PUBLISHERs]
    ([ManagerID]);
GO

-- Creating foreign key on [ManagerID] in table 'STOCK_RECEIVED_NOTE'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE]
ADD CONSTRAINT [FK__STOCK_REC__Manag__76969D2E]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[MANAGERs]
        ([ManagerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__STOCK_REC__Manag__76969D2E'
CREATE INDEX [IX_FK__STOCK_REC__Manag__76969D2E]
ON [dbo].[STOCK_RECEIVED_NOTE]
    ([ManagerID]);
GO

-- Creating foreign key on [ManagerID] in table 'TIERs'
ALTER TABLE [dbo].[TIERs]
ADD CONSTRAINT [FK__TIER__ManagerID__3F466844]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[MANAGERs]
        ([ManagerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TIER__ManagerID__3F466844'
CREATE INDEX [IX_FK__TIER__ManagerID__3F466844]
ON [dbo].[TIERs]
    ([ManagerID]);
GO

-- Creating foreign key on [TierID] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [FK__Person__TierID__44FF419A]
    FOREIGN KEY ([TierID])
    REFERENCES [dbo].[TIERs]
        ([TierID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Person__TierID__44FF419A'
CREATE INDEX [IX_FK__Person__TierID__44FF419A]
ON [dbo].[People]
    ([TierID]);
GO

-- Creating foreign key on [ShipperID] in table 'SHIP_CONFIRMATION'
ALTER TABLE [dbo].[SHIP_CONFIRMATION]
ADD CONSTRAINT [FK__SHIP_CONF__Shipp__2EDAF651]
    FOREIGN KEY ([ShipperID])
    REFERENCES [dbo].[People]
        ([PersonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__SHIP_CONF__Shipp__2EDAF651'
CREATE INDEX [IX_FK__SHIP_CONF__Shipp__2EDAF651]
ON [dbo].[SHIP_CONFIRMATION]
    ([ShipperID]);
GO

-- Creating foreign key on [CustomerID] in table 'WALLETs'
ALTER TABLE [dbo].[WALLETs]
ADD CONSTRAINT [FK__WALLET__Customer__4AB81AF0]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[People]
        ([PersonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__WALLET__Customer__4AB81AF0'
CREATE INDEX [IX_FK__WALLET__Customer__4AB81AF0]
ON [dbo].[WALLETs]
    ([CustomerID]);
GO

-- Creating foreign key on [PublisherID] in table 'STOCK_RECEIVED_NOTE'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE]
ADD CONSTRAINT [FK__STOCK_REC__Publi__75A278F5]
    FOREIGN KEY ([PublisherID])
    REFERENCES [dbo].[PUBLISHERs]
        ([PublisherID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__STOCK_REC__Publi__75A278F5'
CREATE INDEX [IX_FK__STOCK_REC__Publi__75A278F5]
ON [dbo].[STOCK_RECEIVED_NOTE]
    ([PublisherID]);
GO

-- Creating foreign key on [StockReceivedNoteID] in table 'STOCK_RECEIVED_NOTE_DETAIL'
ALTER TABLE [dbo].[STOCK_RECEIVED_NOTE_DETAIL]
ADD CONSTRAINT [FK__STOCK_REC__Stock__7E37BEF6]
    FOREIGN KEY ([StockReceivedNoteID])
    REFERENCES [dbo].[STOCK_RECEIVED_NOTE]
        ([StockReceivedNoteID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__STOCK_REC__Stock__7E37BEF6'
CREATE INDEX [IX_FK__STOCK_REC__Stock__7E37BEF6]
ON [dbo].[STOCK_RECEIVED_NOTE_DETAIL]
    ([StockReceivedNoteID]);
GO

-- Creating foreign key on [WalletID] in table 'TRANSACTION_DETAILS'
ALTER TABLE [dbo].[TRANSACTION_DETAILS]
ADD CONSTRAINT [FK__TRANSACTI__Walle__2645B050]
    FOREIGN KEY ([WalletID])
    REFERENCES [dbo].[WALLETs]
        ([WalletID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TRANSACTI__Walle__2645B050'
CREATE INDEX [IX_FK__TRANSACTI__Walle__2645B050]
ON [dbo].[TRANSACTION_DETAILS]
    ([WalletID]);
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

-- Creating foreign key on [People_PersonID] in table 'MARKED_BOOK'
ALTER TABLE [dbo].[MARKED_BOOK]
ADD CONSTRAINT [FK_MARKED_BOOK_Person]
    FOREIGN KEY ([People_PersonID])
    REFERENCES [dbo].[People]
        ([PersonID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BOOK_EDITION_EditionID] in table 'MARKED_BOOK'
ALTER TABLE [dbo].[MARKED_BOOK]
ADD CONSTRAINT [FK_MARKED_BOOK_BOOK_EDITION]
    FOREIGN KEY ([BOOK_EDITION_EditionID])
    REFERENCES [dbo].[BOOK_EDITION]
        ([EditionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MARKED_BOOK_BOOK_EDITION'
CREATE INDEX [IX_FK_MARKED_BOOK_BOOK_EDITION]
ON [dbo].[MARKED_BOOK]
    ([BOOK_EDITION_EditionID]);
GO

-- Creating foreign key on [OrderID] in table 'CUSTOMER_ORDER_STATUS'
ALTER TABLE [dbo].[CUSTOMER_ORDER_STATUS]
ADD CONSTRAINT [FK__CUSTOMER___Order__16CE6296]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[CUSTOMER_ORDER]
        ([OrderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Order__16CE6296'
CREATE INDEX [IX_FK__CUSTOMER___Order__16CE6296]
ON [dbo].[CUSTOMER_ORDER_STATUS]
    ([OrderID]);
GO

-- Creating foreign key on [StatusID] in table 'CUSTOMER_ORDER_STATUS'
ALTER TABLE [dbo].[CUSTOMER_ORDER_STATUS]
ADD CONSTRAINT [FK__CUSTOMER___Statu__17C286CF]
    FOREIGN KEY ([StatusID])
    REFERENCES [dbo].[ORDER_STATUS]
        ([StatusID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CUSTOMER___Statu__17C286CF'
CREATE INDEX [IX_FK__CUSTOMER___Statu__17C286CF]
ON [dbo].[CUSTOMER_ORDER_STATUS]
    ([StatusID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------