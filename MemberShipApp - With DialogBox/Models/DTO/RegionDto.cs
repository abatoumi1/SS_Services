using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class RegionDto
    {
        public RegionDto()
        {
            StateIDs = new List<TupleData>();
            //IDs = new List<int>();

        }
        [Key]
        public int RegionID { get; set; }
        public int CountryID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
        public List<TupleData> StateIDs { get; set; }
       // public List<int> IDs { get; set; }

    }

    public class TupleData
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
    }

}
