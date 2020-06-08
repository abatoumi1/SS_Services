using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class StatesByRegionsView
    {
        public IEnumerable<RegionDto> regions { get; set; }
        public int CountryID { get; set; }
    }
}
