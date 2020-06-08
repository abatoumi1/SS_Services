using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        [Required]
        public int PositionID { get; set; }
        [Required]
        public int StateID { get; set; }
        public string Code { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 3)]

        [Required]
        public string LastName { get; set; }
        [StringLength(50, MinimumLength = 3)]

        [Required]
        public string Phone { get; set; }

        [Required]
        [StringLength(156, MinimumLength = 3)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        public Position Position { get; set; }
        public State State{ get; set; }
    }
}
