CREATE TABLE [dbo].[ContributionMethod] (
    [ContributionMethodID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ContributionMethod] PRIMARY KEY CLUSTERED ([ContributionMethodID] ASC)
);

