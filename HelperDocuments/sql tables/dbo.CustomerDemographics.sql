USE [Northwind]
GO

/****** Object: Table [dbo].[CustomerDemographics] Script Date: 28.02.2021 15:03:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerDemographics] (
    [CustomerTypeID] NCHAR (10) NOT NULL,
    [CustomerDesc]   NTEXT      NULL
);


