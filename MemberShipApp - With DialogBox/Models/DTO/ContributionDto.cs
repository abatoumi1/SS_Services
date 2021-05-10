using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models.DTO
{
    public class ContributionDto
    {
        public int ContributionID { get; set; }
        public int StateID { get; set; }
        [Required]
        public int MemberID { get; set; }

        [Required]
        public int ContributionMethodID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Contribution Date")]
        public DateTime ContributionDate { get; set; }
    }


    public class Consolidate
    {
        public Consolidate()
        {
            DictCon = new Dictionary<int, List<ContributionDto>>();
        }
        public Dictionary<int, List<ContributionDto>> DictCon { get; set; }
    }
       // Dictionary<string, List<ContributionDto>> Consolidate = new Dictionary<string, List<ContributionDto>>();
    //{
    //    public Consolidate()
    //    {
    //        Contributions = new List<ContributionDto>();
    //    }
    //    public int Year { get; set; }
    //    public decimal YearlyContribution { get; set; }
    //    public List<ContributionDto> Contributions {get;set;}


       
    //}
}
