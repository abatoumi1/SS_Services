CREATE TABLE [dbo].[Member] (
    [MemberID]   INT            IDENTITY (1, 1) NOT NULL,
    [PositionID] INT            NOT NULL,
    [StateID]    INT            NOT NULL,
    [Code]       NVARCHAR (MAX) NULL,
    [FirstName]  NVARCHAR (50)  NOT NULL,
    [LastName]   NVARCHAR (50)  NOT NULL,
    [Phone]      NVARCHAR (50)  NOT NULL,
    [Email]      NVARCHAR (156) NOT NULL,
    [StartDate]  DATETIME2 (7)  NOT NULL,
    [EndDate]    DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED ([MemberID] ASC),
    CONSTRAINT [FK_Member_Position_PositionID] FOREIGN KEY ([PositionID]) REFERENCES [dbo].[Position] ([PositionID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Member_State_StateID] FOREIGN KEY ([StateID]) REFERENCES [dbo].[State] ([StateID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Member_PositionID]
    ON [dbo].[Member]([PositionID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Member_StateID]
    ON [dbo].[Member]([StateID] ASC);

