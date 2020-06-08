using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class Position
    {
        public int PositionID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}
