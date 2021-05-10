using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models.DTO
{
    public class MemberDetails : MemberDto
    {
        public MemberDetails()
        {
            Contributions = new List<ContributionDto>();
        }
        public int RegionID { get; set; }
        public int CountryID { get; set; }
        public string PositionName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public List<ContributionDto> Contributions { get; set;}
    }


    public class MemberContribution : MemberDto
    {
        public MemberContribution()
        {
            Consolidations = new Dictionary<(int, decimal), List<ContributionDto>>();
        }
        public int RegionID { get; set; }
        public int CountryID { get; set; }
        public string PositionName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public Dictionary<(int,decimal), List<ContributionDto>> Consolidations { get; set; }
    }
}
