use BookStoreManager
INSERT INTO TIER (TierName, TierDiscount, TierLevel)
VALUES
    ('Bronze', 0.05, 0),
    ('Silver', 0.10, 150000),
    ('Gold', 0.15, 500000);

insert into AspNetRoles(Id, Name) values
	(1,'Manager'),
	(4,'Customer'),
	(2,'Staff'),
	(3,'Shipper')


select * from book_edition
select * from BOOK_EDITION_IMAGE
select * from STOCK_INVENTORY