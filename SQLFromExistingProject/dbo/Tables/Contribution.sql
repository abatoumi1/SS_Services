CREATE TABLE [dbo].[Contribution] (
    [ContributionID]       INT             IDENTITY (1, 1) NOT NULL,
    [MemberID]             INT             NOT NULL,
    [ContributionMethodID] INT             NOT NULL,
    [Amount]               DECIMAL (18, 2) NOT NULL,
    [ContributionDate]     DATETIME2 (7)   NOT NULL,
    CONSTRAINT [PK_Contribution] PRIMARY KEY CLUSTERED ([ContributionID] ASC),
    CONSTRAINT [FK_Contribution_ContributionMethod_ContributionMethodID] FOREIGN KEY ([ContributionMethodID]) REFERENCES [dbo].[ContributionMethod] ([ContributionMethodID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Contribution_Member_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([MemberID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Contribution_ContributionMethodID]
    ON [dbo].[Contribution]([ContributionMethodID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contribution_MemberID]
    ON [dbo].[Contribution]([MemberID] ASC);

