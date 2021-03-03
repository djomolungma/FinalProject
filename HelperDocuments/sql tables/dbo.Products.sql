USE [Northwind]
GO

/****** Object: Table [dbo].[Products] Script Date: 28.02.2021 15:02:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products] (
    [ProductID]       INT           IDENTITY (1, 1) NOT NULL,
    [ProductName]     NVARCHAR (40) NOT NULL,
    [SupplierID]      INT           NULL,
    [CategoryID]      INT           NULL,
    [QuantityPerUnit] NVARCHAR (20) NULL,
    [UnitPrice]       MONEY         NULL,
    [UnitsInStock]    SMALLINT      NULL,
    [UnitsOnOrder]    SMALLINT      NULL,
    [ReorderLevel]    SMALLINT      NULL,
    [Discontinued]    BIT           NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [CategoriesProducts]
    ON [dbo].[Products]([CategoryID] ASC);


GO
CREATE NONCLUSTERED INDEX [CategoryID]
    ON [dbo].[Products]([CategoryID] ASC);


GO
CREATE NONCLUSTERED INDEX [ProductName]
    ON [dbo].[Products]([ProductName] ASC);


GO
CREATE NONCLUSTERED INDEX [SupplierID]
    ON [dbo].[Products]([SupplierID] ASC);


GO
CREATE NONCLUSTERED INDEX [SuppliersProducts]
    ON [dbo].[Products]([SupplierID] ASC);


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductID] ASC);


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID]);


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID]);


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [DF_Products_UnitPrice] DEFAULT ((0)) FOR [UnitPrice];


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [DF_Products_UnitsInStock] DEFAULT ((0)) FOR [UnitsInStock];


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [DF_Products_UnitsOnOrder] DEFAULT ((0)) FOR [UnitsOnOrder];


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [DF_Products_ReorderLevel] DEFAULT ((0)) FOR [ReorderLevel];


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [DF_Products_Discontinued] DEFAULT ((0)) FOR [Discontinued];


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [CK_Products_UnitPrice] CHECK ([UnitPrice]>=(0));


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [CK_ReorderLevel] CHECK ([ReorderLevel]>=(0));


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [CK_UnitsInStock] CHECK ([UnitsInStock]>=(0));


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [CK_UnitsOnOrder] CHECK ([UnitsOnOrder]>=(0));


