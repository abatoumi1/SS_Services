using MemberShipApp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class Contribution
    {

        public int ContributionID { get; set; }

        [Required]
        public int MemberID { get; set; }

        [Required]
        public int ContributionMethodID { get; set; }

        [Required]        
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Contribution Date")]
        public DateTime ContributionDate { get; set; }

        public Member Member { get; set; }

        public ContributionMethod ContributionMethod { get; set; }
    }
}
