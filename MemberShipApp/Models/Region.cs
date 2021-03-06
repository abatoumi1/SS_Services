﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class Region
    {
        public int RegionID { get; set; }
        public int CountryID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public Country Country { get; set; }
        public ICollection<State> States { get; set; }

    }
}
