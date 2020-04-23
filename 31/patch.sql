USE [OnlineStore]
GO

CREATE OR ALTER PROCEDURE [dbo].[AddOrder](
    @customerId AS INT,
    @orderDate AS DATETIMEOFFSET,
    @discount AS FLOAT = NULL,
    @id AS INT OUTPUT)
AS
Begin
    declare @tableOfId table(id int);

    insert into [dbo].[Order] ([CustomerId], [OrderDate], [Discount])
    output inserted.Id into @tableOfId
    values (@customerId, @orderDate, @discount)

    select @id = id from @tableOfId;
End;
GO

DROP PROCEDURE [dbo].[AddOrderItem];
DROP TYPE OrderItemTableType;
GO

CREATE TYPE OrderItemTableType
   AS TABLE ( orderId INT
            , productId INT
            , numberOfItems INT );
GO

CREATE OR ALTER PROCEDURE [dbo].[AddOrderItem](@tvp as OrderItemTableType readonly)
AS
Begin
    insert into [dbo].[OrderItem] ([OrderId], [ProductId], [NumberOfItems])
    select orderId, productId, numberOfItems from @tvp;
End;
GO
