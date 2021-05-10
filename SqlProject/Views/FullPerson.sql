CREATE VIEW [dbo].[FullPerson]
	AS 
	SELECT [p].[PersonId] as PersonalId, [p].[FirstName], [p].[LastName], 
			[a].[Id], [a].[PersonId], [a].[StreetAddress], 
			[a].[City], [a].[ZipCode], [a].[State] 
	FROM dbo.Person p
	left join dbo.Address a on p.[PersonId] = a.PersonId
