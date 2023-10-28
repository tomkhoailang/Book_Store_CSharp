
go
CREATE function FN_IS_VALID_TO_REVIEW(@currentCustomerID int,@currentEditionID int)
returns bit 
as
begin
	if exists (select * from BOOK_REVIEW where CustomerID = @currentCustomerID and EditionID = @currentEditionID)
	begin
		return 0
	end

	if exists(	select * from CUSTOMER_ORDER CO,CUSTOMER_ORDER_DETAIL COD
	where CO.CustomerID = @currentCustomerID and CO.OrderStatus = 'Success' and CO.OrderID = COD.OrderID and COD.EditionID = @currentEditionID)
	begin
		return 1
	end
	return 0
end
drop function FN_IS_VALID_TO_REVIEW