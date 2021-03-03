USE [Northwind]
GO

/****** Object: Table [dbo].[Users] Script Date: 28.02.2021 15:00:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (50)    NOT NULL,
    [LastName]     VARCHAR (50)    NOT NULL,
    [Email]        VARCHAR (50)    NOT NULL,
    [PasswordHash] VARBINARY (500) NOT NULL,
    [PasswordSalt] VARBINARY (500) NOT NULL,
    [Status]       BIT             NOT NULL
);


