using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models.DTO
{
    public class MemberDto
    {
        public int MemberID { get; set; }

        [Display(Name = "Position")]
        [Required]
        public int PositionID { get; set; }

        [Display(Name = "State Name")]
        [Required]
        public int StateID { get; set; }

        [Display(Name = "Member ID Code")]
        [Required]
        public string Code { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(50, MinimumLength = 3)]
        public string Phone { get; set; }

        [Display(Name = "Memeber Email")]
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
        public State State { get; set; }
    }
}
