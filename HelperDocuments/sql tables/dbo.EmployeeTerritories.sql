USE [Northwind]
GO

/****** Object: Table [dbo].[EmployeeTerritories] Script Date: 28.02.2021 15:02:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeeTerritories] (
    [EmployeeID]  INT           NOT NULL,
    [TerritoryID] NVARCHAR (20) NOT NULL
);


