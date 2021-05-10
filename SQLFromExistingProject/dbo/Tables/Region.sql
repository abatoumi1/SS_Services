CREATE TABLE [dbo].[Region] (
    [RegionID]    INT            IDENTITY (1, 1) NOT NULL,
    [CountryID]   INT            NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (256) NULL,
    CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED ([RegionID] ASC),
    CONSTRAINT [FK_Region_Country_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([CountryID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Region_CountryID]
    ON [dbo].[Region]([CountryID] ASC);

