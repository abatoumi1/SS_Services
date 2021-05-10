
using MemberShipApp.Extensions;
using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.Data;

namespace MemberShipApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MemberShipContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            //if (context.Countries.Any())
            //{
            //    return;   // DB has been seeded
            //}

            var countries = new Country[]
            {
                new Country { Name = "United States", Code = "USA"},
                new Country { Name = "France", Code = "FR"},
                new Country { Name = "Cameroon", Code = "CMR"},
                new Country { Name = "Canada", Code = "CA"},
                new Country { Name = "Italy", Code = "IT"},

            };
            if (!context.Countries.Any())
            {
                foreach (Country c in countries)
                {
                    context.Countries.Add(c);
                }
                context.SaveChanges();
            }

            var positions = new Position[]
            {
                new Position { Name = "Secretary"},
                new Position { Name = "Financial Secretary"},
                new Position { Name = "Tresaury"},
                new Position { Name = "Coordinator"},
                new Position { Name = "Member"}
            };
            if (!context.Positions.Any())
            {
                foreach (Position i in positions)
                {
                    context.Positions.Add(i);
                }
                context.SaveChanges();
            }
            var regions = new Region[]
            {
                new Region { Name = "USA-NorthEast",     CountryID  = countries.Single( i => i.Code == "USA").CountryID },
                new Region { Name = "USA-SouthEast",     CountryID  = countries.Single( i => i.Code == "USA").CountryID },
                new Region { Name = "USA-MidWest",     CountryID  = countries.Single( i => i.Code == "USA").CountryID },
                new Region { Name = "USA-SouthWest",     CountryID  = countries.Single( i => i.Code == "USA").CountryID },
                new Region { Name = "USA-West",     CountryID  = countries.Single( i => i.Code == "USA").CountryID },
                 new Region { Name = "CA-Atlantic",     CountryID  = countries.Single( i => i.Code == "CA").CountryID },
                new Region { Name = "CA-Central",     CountryID  = countries.Single( i => i.Code == "CA").CountryID },
                new Region { Name = "CA-Prairie",     CountryID  = countries.Single( i => i.Code == "CA").CountryID },
                new Region { Name = "CA-West",     CountryID  = countries.Single( i => i.Code == "CA").CountryID },
                new Region { Name = "CA-North",     CountryID  = countries.Single( i => i.Code == "CA").CountryID },
            };

            if (!context.Regions.Any())
            {
                foreach (Region d in regions)
                {
                    context.Regions.Add(d);
                }
                context.SaveChanges();
            }

            var RegionID = regions.FirstOrDefault(s => s.Name == "USA-SouthEast");//.RegionID;
            var states = new State[]
            {
               


                new State  {
                    Name = "Alabama",
                    Code = "AL",
                    RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Alaska",
                   Code = "AK",
                    RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },
                new State{
                    Name= "American Samoa",
                   Code = "AS",
                   RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },
                new State{
                    Name= "Arizona",
                   Code = "AZ",
                   RegionID = regions.Single( s => s.Name == "USA-SouthWest").RegionID
                },
                new State{
                    Name= "Arkansas",
                   Code = "AR",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "California",
                   Code = "CA",
                   RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },
                new State{
                    Name= "Colorado",
                   Code = "CO",
                   RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },
                new State{
                    Name= "Connecticut",
                   Code = "CT",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "Delaware",
                   Code = "DE",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "District Of Columbia",
                   Code = "DC",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "Federated States Of Micronesia",
                   Code = "FM",
                    RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Florida",
                   Code = "FL",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Georgia",
                   Code = "GA",
                    RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Guam",
                   Code = "GU",
                    RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Hawaii",
                   Code = "HI",
                    RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Idaho",
                   Code = "ID",
                    RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },
                new State{
                    Name= "Illinois",
                   Code = "IL",
                    RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Indiana",
                   Code = "IN",
                    RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Iowa",
                   Code = "IA",
                    RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Kansas",
                   Code = "KS",
                    RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Kentucky",
                   Code = "KY",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Louisiana",
                   Code = "LA",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Maine",
                   Code = "ME",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "Marshall Islands",
                   Code = "MH",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Maryland",
                   Code = "MD",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "Massachusetts",
                   Code = "MA",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "Michigan",
                   Code = "MI",
                   RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Minnesota",
                   Code = "MN",
                   RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Mississippi",
                   Code = "MS",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Missouri",
                   Code = "MO",
                   RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Montana",
                   Code = "MT",
                   RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },
                new State{
                    Name= "Nebraska",
                   Code = "NE",
                   RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Nevada",
                   Code = "NV",
                   RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },
                new State{
                    Name= "New Hampshire",
                   Code = "NH",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "New Jersey",
                   Code = "NJ",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "New Mexico",
                   Code = "NM",
                   RegionID = regions.Single( s => s.Name == "USA-SouthWest").RegionID
                },
                new State{
                    Name= "New York",
                   Code = "NY",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "North Carolina",
                   Code = "NC",
                    RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "North Dakota",
                   Code = "ND",
                    RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Northern Mariana Islands",
                   Code = "MP",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID

                },
                new State{
                    Name= "Ohio",
                   Code = "OH",
                   RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Oklahoma",
                   Code = "OK",
                   RegionID = regions.Single( s => s.Name == "USA-SouthWest").RegionID
                },
                new State{
                    Name= "Oregon",
                   Code = "OR",
                   RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },
                new State{
                    Name= "Palau",
                   Code = "PW",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Pennsylvania",
                   Code = "PA",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "Puerto Rico",
                   Code = "PR",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Rhode Island",
                   Code = "RI",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "South Carolina",
                   Code = "SC",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "South Dakota",
                   Code = "SD",
                   RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Tennessee",
                   Code = "TN",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Texas",
                   Code = "TX",
                   RegionID = regions.Single( s => s.Name == "USA-SouthWest").RegionID
                },
                new State{
                    Name= "Utah",
                   Code = "UT",
                   RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },
                new State{
                    Name= "Vermont",
                   Code = "VT",
                   RegionID = regions.Single( s => s.Name == "USA-NorthEast").RegionID
                },
                new State{
                    Name= "Virgin Islands",
                   Code = "VI",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Virginia",
                   Code = "VA",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Washington",
                   Code = "WA",
                   RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },
                new State{
                    Name= "West Virginia",
                   Code = "WV",
                   RegionID = regions.Single( s => s.Name == "USA-SouthEast").RegionID
                },
                new State{
                    Name= "Wisconsin",
                   Code = "WI",
                   RegionID = regions.Single( s => s.Name == "USA-MidWest").RegionID
                },
                new State{
                    Name= "Wyoming",
                   Code = "WY",
                   RegionID = regions.Single( s => s.Name == "USA-West").RegionID
                },

                 new State{
                    Name= "Newfoundland and Labrador",
                   Code = "CA-NL",
                   RegionID = regions.Single( s => s.Name == "CA-Atlantic").RegionID
                },

                new State{
                    Name= "Prince Edward Island",
                   Code = "CA-PEI",
                   RegionID = regions.Single( s => s.Name == "CA-Atlantic").RegionID
                },
                 new State{
                    Name= "Nova Scotia",
                   Code = "CA-NS",
                   RegionID = regions.Single( s => s.Name == "CA-Atlantic").RegionID
                },
                new State{
                    Name= "New Brunswick",
                   Code = "CA-NB",
                   RegionID = regions.Single( s => s.Name == "CA-Atlantic").RegionID
                },
                new State{
                    Name= "Ontario",
                   Code = "ON",
                   RegionID = regions.Single( s => s.Name == "CA-Central").RegionID
                },
                new State{
                    Name= "Quebec",
                   Code = "CA-QU",
                   RegionID = regions.Single( s => s.Name == "CA-Central").RegionID
                },
                 new State{
                   Name= "Manitoba",
                   Code = "Ma",
                   RegionID = regions.Single( s => s.Name == "CA-Prairie").RegionID
                },
                 new State{
                   Name= "Saskatchewan",
                   Code = "CA-SA",
                   RegionID = regions.Single( s => s.Name == "CA-Prairie").RegionID
                },
                  new State{
                   Name= "Alberta",
                   Code = "CA-AL",
                   RegionID = regions.Single( s => s.Name == "CA-Prairie").RegionID
                },
                new State{
                   Name= "British Columbia",
                   Code = "CA-BC",
                   RegionID = regions.Single( s => s.Name == "CA-West").RegionID
                },
                 new State{
                   Name= "Nunavut",
                   Code = "CA-NU",
                   RegionID = regions.Single( s => s.Name == "CA-North").RegionID
                },

                 new State{
                   Name= "Northwest Territories",
                   Code = "CA-NT",
                   RegionID = regions.Single( s => s.Name == "CA-North").RegionID
                },
                new State{
                   Name= "Yukon Territory",
                   Code = "CA-YT",
                   RegionID = regions.Single( s => s.Name == "CA-North").RegionID
                },
            };

            if (!context.States.Any())
            {
                try
                {
                    foreach (State c in states)
                    {
                        context.States.Add(c);
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            

            var members = new Member[]
            {
                new Member {
                    FirstName="Alph",
                    LastName ="Boutman",
                    Code = "MD-01",
                    Email ="adonisbatoumi@yahoo.fr",
                    Phone = "088-789-9876",
                    StartDate = DateTime.Now,
                    StateID = states.Single(s => s.Code == "MD").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID
                    
                },
                   new Member {
                    FirstName="Alph01",
                    LastName ="Boutman01",
                    Code = "MD-02",
                    Email ="adonisbatoumi1@yahoo.fr",
                    StartDate = DateTime.Now,
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "MD").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                    FirstName="Alph02",
                    LastName ="Boutman02",
                    Code = "MD-03",
                    Email ="adonisbatoumi@yahoo.fr",
                    Phone = "088-789-9876",
                    StartDate = DateTime.Now,
                    StateID = states.Single(s => s.Code == "MD").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                    FirstName="Alph03",
                    LastName ="Boutman03",
                    Code = "MD-04",
                    StartDate = DateTime.Now,
                    Email ="adonisbatoumi1@yahoo.fr",
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "MD").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },

                   new Member {
                    FirstName="Alph08",
                    LastName ="Boutman08",
                    Code = "MD-09",
                    StartDate = DateTime.Now,
                    Email ="adonisbatoumi@yahoo.fr",
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "CA").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                    FirstName="Alph07",
                    LastName ="Boutman07",
                    Code = "MD-08",
                    StartDate = DateTime.Now,
                    Email ="adonisbatoumi1@yahoo.fr",
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "CA").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                    FirstName="Alph06",
                    LastName ="Boutman06",
                    Code = "MD-07",
                    StartDate = DateTime.Now,
                    Email ="adonisbatoumi@yahoo.fr",
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "CA").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                    FirstName="Alph04",
                    LastName ="Boutman04",
                    Code = "MD-05",
                    StartDate = DateTime.Now,
                    Email ="adonisbatoumi1@yahoo.fr",
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "CA").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },

//*********************************************************************//
                 new Member {
                    FirstName=GeneratedName.GetName(4),
                    LastName =GeneratedName.GetName(8),
                    Code = "MD-01",
                    Email ="adonisbatoumi@yahoo.fr",
                    Phone = "088-789-9876",
                    StartDate = DateTime.Now,
                    StateID = states.Single(s => s.Code == "AL").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                    FirstName=GeneratedName.GetName(4),
                    LastName =GeneratedName.GetName(8),
                    Code = "MD-02"+GeneratedName.GetName(4),
                    Email ="adonisbatoumi1@yahoo.fr",
                    StartDate = DateTime.Now,
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "GA").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                     FirstName=GeneratedName.GetName(4),
                    LastName =GeneratedName.GetName(8),
                    Code = "MD-03"+GeneratedName.GetName(4),
                    Email ="adonisbatoumi@yahoo.fr",
                    Phone = "088-789-9876",
                    StartDate = DateTime.Now,
                    StateID = states.Single(s => s.Code == "PA").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                    FirstName=GeneratedName.GetName(4),
                    LastName =GeneratedName.GetName(8),
                    Code = "MD-04"+GeneratedName.GetName(4),
                    StartDate = DateTime.Now,
                    Email ="adonisbatoumi1@yahoo.fr",
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "NV").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },

                   new Member {
                     FirstName=GeneratedName.GetName(4),
                    LastName =GeneratedName.GetName(8),
                    Code = "MD-09"+GeneratedName.GetName(4),
                    StartDate = DateTime.Now,
                    Email ="adonisbatoumi@yahoo.fr",
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "NY").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                    FirstName=GeneratedName.GetName(4),
                    LastName =GeneratedName.GetName(8),
                    Code = "MD-08"+GeneratedName.GetName(4),
                    StartDate = DateTime.Now,
                    Email ="adonisbatoumi1@yahoo.fr",
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "NY").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                     FirstName=GeneratedName.GetName(4),
                    LastName =GeneratedName.GetName(8),
                    Code = "MD-07"+GeneratedName.GetName(4),
                    StartDate = DateTime.Now,
                    Email ="adonisbatoumi@yahoo.fr",
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "NY").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },
                   new Member {
                     FirstName=GeneratedName.GetName(6),
                    LastName =GeneratedName.GetName(12),
                    Code = "MD-05-"+GeneratedName.GetName(4),
                    StartDate = DateTime.Now,
                    Email ="adonisbatoumi1@yahoo.fr",
                    Phone = "088-789-9876",
                    StateID = states.Single(s => s.Code == "NY").StateID,
                    PositionID = positions.Single(c => c.Name== "Member" ).PositionID

                },

                   //********************************************************//

            };
            if (!context.Members.Any())
            {
                foreach (Member e in members)
                {
                    context.Add(e);
                }
                context.SaveChanges();
            }

            /*******************/

            var contributionMethods = new ContributionMethod[]
            {
                new ContributionMethod { Name = "Credit Card"},
                new ContributionMethod { Name = "PayPal"},
                new ContributionMethod { Name = "Cash"},
                new ContributionMethod { Name = "Wire Transfer"},
            };
            if (!context.ContributionMethods.Any())
            {
                foreach (ContributionMethod c in contributionMethods)
                {
                    context.ContributionMethods.Add(c);
                }
                context.SaveChanges();
            }

            /*******************************************/

            var contributions = new Contribution[]
           {
                new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=50, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "Cash").ContributionMethodID},
                new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=60, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "PayPal").ContributionMethodID},
                new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=70, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "Credit Card").ContributionMethodID},
                new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=40, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "Cash").ContributionMethodID},
                 new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=50, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "Cash").ContributionMethodID},
                new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=60, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "PayPal").ContributionMethodID},
                new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=70, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "Credit Card").ContributionMethodID},
                new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=40, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "Cash").ContributionMethodID},
                 new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=50, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "Cash").ContributionMethodID},
                new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=60, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "PayPal").ContributionMethodID},
                new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=70, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "Credit Card").ContributionMethodID},
                new Contribution { MemberID = members.FirstOrDefault().MemberID, Amount=40, ContributionDate=DateTime.Now, ContributionMethodID=contributionMethods.Single(s => s.Name == "Cash").ContributionMethodID},
           };
            if (!context.Contributions.Any())
            {
                foreach (Contribution c in contributions)
                {
                    context.Contributions.Add(c);
                }
                context.SaveChanges();
            }
        }
    }
}