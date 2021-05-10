using MemberShipApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class MemberContributionView
    {
        public MemberContributionView()
        {
            Members = new List<MemberYearlyContribution>();
            Countries = new List<CountryTuplate>();
        }
        public List<MemberYearlyContribution> Members { get; set; }
        public List<CountryTuplate> Countries { get; set; }
        public MemberDto SelectedMember { get; set; }
        public int SelectedCountryID { get; set; }
        public int SelectedRegionID { get; set; }
        public int SelectedStateID { get; set; }
    }
}
