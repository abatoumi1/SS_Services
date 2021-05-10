CREATE TABLE [dbo].[State] (
    [StateID]  INT           IDENTITY (1, 1) NOT NULL,
    [RegionID] INT           NOT NULL,
    [Code]     NVARCHAR (10) NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([StateID] ASC),
    CONSTRAINT [FK_State_Region_RegionID] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([RegionID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_State_RegionID]
    ON [dbo].[State]([RegionID] ASC);

