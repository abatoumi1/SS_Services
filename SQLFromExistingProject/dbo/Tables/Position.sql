CREATE TABLE [dbo].[Position] (
    [PositionID]  INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (256) NULL,
    CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED ([PositionID] ASC)
);

