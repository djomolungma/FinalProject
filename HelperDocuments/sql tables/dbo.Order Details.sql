USE [Northwind]
GO

/****** Object: Table [dbo].[Order Details] Script Date: 28.02.2021 15:02:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order Details] (
    [OrderID]   INT      NOT NULL,
    [ProductID] INT      NOT NULL,
    [UnitPrice] MONEY    NOT NULL,
    [Quantity]  SMALLINT NOT NULL,
    [Discount]  REAL     NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [OrderID]
    ON [dbo].[Order Details]([OrderID] ASC);


GO
CREATE NONCLUSTERED INDEX [OrdersOrder_Details]
    ON [dbo].[Order Details]([OrderID] ASC);


GO
CREATE NONCLUSTERED INDEX [ProductID]
    ON [dbo].[Order Details]([ProductID] ASC);


GO
CREATE NONCLUSTERED INDEX [ProductsOrder_Details]
    ON [dbo].[Order Details]([ProductID] ASC);


GO
ALTER TABLE [dbo].[Order Details]
    ADD CONSTRAINT [PK_Order_Details] PRIMARY KEY CLUSTERED ([OrderID] ASC, [ProductID] ASC);


GO
ALTER TABLE [dbo].[Order Details]
    ADD CONSTRAINT [FK_Order_Details_Orders] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Orders] ([OrderID]);


GO
ALTER TABLE [dbo].[Order Details]
    ADD CONSTRAINT [FK_Order_Details_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID]);


GO
ALTER TABLE [dbo].[Order Details]
    ADD CONSTRAINT [DF_Order_Details_UnitPrice] DEFAULT ((0)) FOR [UnitPrice];


GO
ALTER TABLE [dbo].[Order Details]
    ADD CONSTRAINT [DF_Order_Details_Quantity] DEFAULT ((1)) FOR [Quantity];


GO
ALTER TABLE [dbo].[Order Details]
    ADD CONSTRAINT [DF_Order_Details_Discount] DEFAULT ((0)) FOR [Discount];


GO
ALTER TABLE [dbo].[Order Details]
    ADD CONSTRAINT [CK_Discount] CHECK ([Discount]>=(0) AND [Discount]<=(1));


GO
ALTER TABLE [dbo].[Order Details]
    ADD CONSTRAINT [CK_Quantity] CHECK ([Quantity]>(0));


GO
ALTER TABLE [dbo].[Order Details]
    ADD CONSTRAINT [CK_UnitPrice] CHECK ([UnitPrice]>=(0));


