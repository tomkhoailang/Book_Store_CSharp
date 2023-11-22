use BookStoreManager;
--tier
insert into TIER values('S',20,1,1)

select * from tier
--order_status
insert into ORDER_STATUS values ('INITIAL'),
('WAITING'),
('CANCEL BY CUSTOMER'),
('PROCESSING'),
('IS AVAILABLE AT STORE'),
('DELIVERING'),
('SUCCESS'),
('CANCEL BECAUSE OF MANY FAILED DELIVERING')

--person
insert into Person values('Duy', 'aaaa', 'ACTIVE', '45dfe5a4-58c1-4751-9aa0-c87b9dcf7b66', 2, 1)
select * from Person
--wallet
insert into WALLET values(3,10000000)
update WALLET set Balance = 1000000 where WalletID = 1
select * from wallet
--category
insert into CATEGORY(CategoryName, CategoryDescription, ManagerID) values ('Kinh di', 'aaa', 1)
select * from CATEGORY
--book_category
insert into BOOK_CATEGORY values(3,3),(4,3),(5,3)
--promotion
insert into PROMOTION values('giam gia', 10, '2023-11-11', '2023-12-12', 0, 1, 'aaa')
select * from PROMOTION
--book_promotion
insert into BOOK_PROMOTION values(1,3),(1,4),(1,5)
--publisher
insert into PUBLISHER values('kim dong', 'bbb', 'image.jpg', 1)
select * from PUBLISHER
--stock_received_note
insert into STOCK_RECEIVED_NOTE values (GETDATE(),1,1)
select * from STOCK_RECEIVED_NOTE
--stock_received_note_details
select * from STOCK_RECEIVED_NOTE_DETAIL
delete from STOCK_RECEIVED_NOTE_DETAIL
insert into STOCK_RECEIVED_NOTE_DETAIL values (1000, 10000000, 3, 1)
insert into STOCK_RECEIVED_NOTE_DETAIL values (1000, 10000000, 4, 1)
insert into STOCK_RECEIVED_NOTE_DETAIL values (1000, 10000000, 5, 1)

--book_collection
insert into BOOK_COLLECTION values ('thuong', 1), ('dac biet', 1), ('sieu cap vip pro', 1)
select * from BOOK_COLLECTION
--book_edition
insert into BOOK_EDITION values (100000,'hihihi123',2002,300, 'nhom truong nhu c', 'le van a', 1, 2),
								(120000,'h2hihi',2010,300, 'naruto', 'le van b', 1, 3),
								(200000,'hi3242hi',2000,300, 'gi gi do', 'le van c', 1, 1)
select * from BOOK_EDITION
--book-edition_image
insert into BOOK_EDITION_IMAGE values ('image1.jpg',3),('image2.jpg',4),('image3.jpg',5)

--stock-inventory
insert into STOCK_INVENTORY values (1000,0,1000,4),(1000,0,1000,5)
select * from STOCK_INVENTORY
update STOCK_INVENTORY set InventoryStockOutTotal = 99 where EditionID = 6
--cústom-order-detail
insert into CUSTOMER_ORDER_DETAIL(DetailCurrentPrice,DetailQuantity,OrderID,EditionID) values(10000,2,1,4)

select * from AspNetUsers
select * from CUSTOMER_ORDER
select * from CUSTOMER_ORDER_DETAIL

delete from CUSTOMER_ORDER_DETAIL
delete from CUSTOMER_ORDER

drop table CUSTOMER_ORDER_DETAIL
drop table CUSTOMER_ORDER
drop table TRANSACTION_DETAILS
drop table SHIP_CONFIRMATION
drop table CUSTOMER_ORDER_STATUS
drop table ORDER_STATUS