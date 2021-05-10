CREATE PROCEDURE [dbo].[spPerson_FilteredByLastName]
	@LastName nvarchar(50)	
AS
BEGIN
	SELECT [PersonId], [FirstName], [LastName]
	FROM dbo.Person
	WHERE LastName = @LastName;
END
