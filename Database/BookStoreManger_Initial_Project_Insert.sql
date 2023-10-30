use BookStoreManager

ALTER TABLE Person ADD CONSTRAINT FK_Person_AspNetUsers FOREIGN KEY (AccountID) REFERENCES AspNetUsers(Id)
ALTER TABLE MANAGER ADD CONSTRAINT FK_MANAGER_AspNetUsers FOREIGN KEY (AccountID) REFERENCES AspNetUsers(Id)

select * from aspnetroles
select * from aspnetusers
select * from manager

INSERT INTO MANAGER(AccountID) VALUES ('d43cf3c3-b308-4b6d-8481-a514a14222c5')

select * from CATEGORY
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

select * from tier

INSERT INTO TIER (TierName, TierDiscount, TierLevel)
VALUES
    ('Bronze', 0.00, 0),
    ('Silver', 0.10, 150000),
    ('Gold', 0.15, 500000);

insert into AspNetRoles(Id, Name) values
	(1,'Manager'),
	(4,'Customer'),
	(2,'Staff'),
	(3,'Shipper')

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




