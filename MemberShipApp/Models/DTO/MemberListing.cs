using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models.DTO
{
    public class MemberListing: MemberDto
    {
        public int RegionID { get; set; }
        public int CountryID { get; set; }
    }
}
