using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models.DTO
{
    public class MemberYearlyContribution
    {
        
        public int MemberID { get; set; } 
        public int CountryID { get; set; }
        public int RegionID { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public int StateID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }    
        public decimal YearlyContribution { get; set; }
    }
}
