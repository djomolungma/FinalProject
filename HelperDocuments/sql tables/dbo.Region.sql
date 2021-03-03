USE [Northwind]
GO

/****** Object: Table [dbo].[Region] Script Date: 28.02.2021 15:02:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Region] (
    [RegionID]          INT        NOT NULL,
    [RegionDescription] NCHAR (50) NOT NULL
);


