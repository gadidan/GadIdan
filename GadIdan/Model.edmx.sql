
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/16/2015 23:38:23
-- Generated from EDMX file: C:\Users\user\Documents\Visual Studio 2013\Projects\GadIdan\GadIdan\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [genifacedb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Attractions_Locations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attractions] DROP CONSTRAINT [FK_Attractions_Locations];
GO
IF OBJECT_ID(N'[dbo].[FK_Attractions_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attractions] DROP CONSTRAINT [FK_Attractions_Sites];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Attractions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attractions];
GO
IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[Sites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sites];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Attractions'
CREATE TABLE [dbo].[Attractions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AttractionID] int  NOT NULL,
    [AttractionName] nvarchar(150)  NOT NULL,
    [LocationID] int  NOT NULL,
    [AttractionDate] datetime  NOT NULL,
    [AttractionPrice] decimal(18,2)  NOT NULL,
    [Price] decimal(18,2)  NOT NULL,
    [SiteID] int  NOT NULL,
    [AttractionSiteUrl] nvarchar(500)  NOT NULL,
    [AttractionData1] nvarchar(500)  NULL,
    [AttractionData2] nvarchar(500)  NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [LocationID] int IDENTITY(1,1) NOT NULL,
    [LocationName] nvarchar(150)  NOT NULL,
    [LocationAddress] nvarchar(500)  NOT NULL,
    [LocationData1] nvarchar(500)  NULL,
    [LocationData2] nvarchar(500)  NULL
);
GO

-- Creating table 'Sites'
CREATE TABLE [dbo].[Sites] (
    [SiteID] int IDENTITY(1,1) NOT NULL,
    [SiteName] nvarchar(150)  NOT NULL,
    [SiteUrl] nvarchar(500)  NOT NULL,
    [SiteData] nvarchar(500)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Attractions'
ALTER TABLE [dbo].[Attractions]
ADD CONSTRAINT [PK_Attractions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LocationID] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([LocationID] ASC);
GO

-- Creating primary key on [SiteID] in table 'Sites'
ALTER TABLE [dbo].[Sites]
ADD CONSTRAINT [PK_Sites]
    PRIMARY KEY CLUSTERED ([SiteID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LocationID] in table 'Attractions'
ALTER TABLE [dbo].[Attractions]
ADD CONSTRAINT [FK_Attractions_Locations]
    FOREIGN KEY ([LocationID])
    REFERENCES [dbo].[Locations]
        ([LocationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Attractions_Locations'
CREATE INDEX [IX_FK_Attractions_Locations]
ON [dbo].[Attractions]
    ([LocationID]);
GO

-- Creating foreign key on [SiteID] in table 'Attractions'
ALTER TABLE [dbo].[Attractions]
ADD CONSTRAINT [FK_Attractions_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[Sites]
        ([SiteID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Attractions_Sites'
CREATE INDEX [IX_FK_Attractions_Sites]
ON [dbo].[Attractions]
    ([SiteID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------