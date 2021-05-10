using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadDowloadExcelFile.Models;

namespace UploadDowloadExcelFile.Shared
{
    public static  class Data
    {
        public static List<Member> GetMember()
        {
            List<Member> members = new List<Member>();
            int j = 300;
            while (j > 0)
            {
                var mem = new Member
                {
                    MemberID = j,
                    FirstName = $"Adonis-{DateTime.Now.Ticks}-01{j}",
                    LastName = $"Sonia123{j}-01{j}",
                    Code = $"MD-00A-{j}",
                    Email = $"adonis123{j}@gmail.com",
                    Phone = $"088-789-98{j}",
                    StartDate = DateTime.Now,
                    IsActive = true,

                    OwnerID = DateTime.Now.Ticks.ToString(),
                    Role = MemberLoginRole.Member,
                    Status = MemberLoginStatus.Approved

                };
                members.Add(mem);
                j--;
            }


            var member = new Member[]
                   {
                new Member {
                    MemberID=301,
                    FirstName="Admin",
                    LastName ="Admin",
                    Code = "MD-00A",
                    Email ="admin@gmail.com",
                    Phone = "088-789-9876",
                    StartDate = DateTime.Now,
                    IsActive =true,
                    OwnerID=DateTime.Now.Ticks.ToString(),
                    Role = MemberLoginRole.Admin,
                    Status = MemberLoginStatus.Approved


                },
                   new Member {
                       MemberID=302,
                    FirstName="Executive",
                    LastName ="Executive",
                    Code = "MD-00E",
                    Email ="executive@gmail.com",
                    StartDate = DateTime.Now,
                    Phone = "088-789-9876",
                    IsActive =true,
                    OwnerID= DateTime.Now.Ticks.ToString(),
                    Role = MemberLoginRole.Executive,
                    Status = MemberLoginStatus.Approved

                },
                   new Member {
                       MemberID=303,
                    FirstName="Member",
                    LastName ="Member",
                    Code = "MD-00M",
                    Email ="member@gmail.com",
                    Phone = "088-789-9876",
                    StartDate = DateTime.Now,
                    IsActive =true,
                    OwnerID=DateTime.Now.Ticks.ToString(),
                    Role = MemberLoginRole.Member,
                    Status = MemberLoginStatus.Approved
                   }
                };
            members.AddRange(member);
            return members;
        }


        public static List<ContributionByMember> GetContribution()
        {
            var contributionTypes = new ContributionType[]
            {
                new ContributionType {FixAmount=700,ContributionTypeID=1, Name = "RegistrationFee"},
                new ContributionType {FixAmount=150,ContributionTypeID=2, Name = "SAGI"},
                new ContributionType {FixAmount=1000,ContributionTypeID=3, Name = "Saving"},
                new ContributionType {FixAmount=10,ContributionTypeID=4, Name = "Sanction"},
                new ContributionType {FixAmount=70,ContributionTypeID=5, Name = "Miscellaneous"},
                new ContributionType {FixAmount=120,ContributionTypeID=6, Name = "Sickness", GroupType="Assistance"},
                new ContributionType {FixAmount=30,ContributionTypeID=7, Name = "Death", GroupType="Assistance"},
                new ContributionType {FixAmount=50,ContributionTypeID=8, Name = "Birth", GroupType="Assistance"},
                new ContributionType {FixAmount=200,ContributionTypeID=9, Name = "DjanguiI", GroupType="Djangui"},
                new ContributionType {FixAmount=500,ContributionTypeID=10, Name = "DjanguiII", GroupType="Djangui"},

            };
            List<ContributionByMember> members = new List<ContributionByMember>();
            for(int i=1; i < 100; i++)
            {
                var date = DateTime.Now.Ticks;
                foreach (var ct in contributionTypes)
                {
                    var mem = new ContributionByMember
                    {
                        MemberID = i,
                        FirstName = $"Adonis-{date}-01{i}",
                        LastName = $"Sonia123{i}-01{i}",
                        Amount =ct.FixAmount ,
                        ContributionTypeID = ct.ContributionTypeID,
                        ContributionTypeName = ct.Name,
                        ContributionDate = DateTime.Now


                    };
                    var mem1 = new ContributionByMember
                    {
                        MemberID = i,
                        FirstName = $"Adonis-{date}-01{i}",
                        LastName = $"Sonia123{i}-01{i}",
                        Amount = ct.FixAmount,
                        ContributionTypeID = ct.ContributionTypeID,
                        ContributionTypeName = ct.Name,
                        ContributionDate = DateTime.Now


                    };
                    members.Add(mem);
                    members.Add(mem1);
                }

            }          
            return members;
        }
    }
}
