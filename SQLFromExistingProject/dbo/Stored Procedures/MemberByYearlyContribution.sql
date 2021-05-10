CREATE PROCEDURE [dbo].[MemberByYearlyContribution]
	  (
		@countryID INT,
		@year INT
		)
		AS
		BEGIN
			SELECT  
				MemCont.MemberID, 
				MemCont.FirstName, 
				MemCont.LastName, 
				MemCont.Phone,
				MemCont.Email,
				MemCont.StateID,
				Co.CountryID, 
				Re.RegionID, 
				St.Name AS StateName,
				Co.Name AS CountryName, 
				Re.Name As RegionName, 
				Sum( MemCont.Amount) as YearlyContribution
			FROM Country as Co, State as St, Region as Re,(
			SELECT 
				Mem.MemberID, 
				Mem.FirstName, 
				Mem.LastName,
				Mem.Phone,
				Mem.Email,
				Mem.StateID, 
				Cont.ContributionDate, 
				cont.Amount
			FROM Member as Mem
			LEFT OUTER JOIN(
			SELECT *
			FROM Contribution as Con
			Where Year(Con.ContributionDate)=@year) as Cont
			ON  Cont.MemberID=Mem.MemberID) as MemCont
			Where Co.CountryID=@countryID 
			AND Re.CountryID=Co.CountryID
			AND St.RegionID=Re.RegionID
			AND St.StateID=MemCont.StateID
			GROUP BY 
				MemCont.MemberID, 
				MemCont.FirstName, 
				MemCont.LastName, 
				MemCont.StateID,
				MemCont.Phone,
				MemCont.Email,
				St.StateID, 
				Co.CountryID, 
				Re.RegionID,
				St.Name,
				Co.Name, 
				Re.Name
			ORDER BY  MemCont.MemberID
		END;