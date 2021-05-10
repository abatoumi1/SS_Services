using MemberShipApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class MemberListingView
    {

        public MemberListingView()
        {
            Members = new List<MemberListing>();
            Countries = new List<CountryTuplate>();
            //Regions = new List<RegionDto>();
            //States = new List<StateDto>();
        }
        public List<MemberListing> Members { get; set; }
        public List<CountryTuplate> Countries { get; set; }
        //public List<StateDto> States { get; set; }
        //public List<RegionDto> Regions { get; set; }
        public MemberDto SelectedMember { get; set; }
        public int SelectedCountry { get; set; }
    }
}
