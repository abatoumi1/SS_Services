CREATE TABLE [dbo].[Country] (
    [CountryID] INT           IDENTITY (1, 1) NOT NULL,
    [Code]      NVARCHAR (10) NULL,
    [Name]      NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([CountryID] ASC)
);

