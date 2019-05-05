
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/01/2014 14:11:05
-- Generated from EDMX file: D:\Projekty\Magisterka\Magisterka\Magisterka.Data.Access\EF\MagisterkaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Magisterka];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[LastPoints]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LastPoints];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LastPoints'
CREATE TABLE [dbo].[LastPoints] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartPointId] int  NOT NULL,
    [EndPointId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LastPoints'
ALTER TABLE [dbo].[LastPoints]
ADD CONSTRAINT [PK_LastPoints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------