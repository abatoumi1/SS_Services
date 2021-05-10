using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class Country
    {
        public int CountryID { get; set; }

        [StringLength(10, MinimumLength = 2)]
        public string Code { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        public ICollection<Region> Regions { get; set; }
    }
}
