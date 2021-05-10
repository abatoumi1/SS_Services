using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadDowloadExcelFile.Models
{
    public class ContributionByMember
    {
        public int MemberID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ContributionTypeID { get; set; }
        public string ContributionTypeName { get; set; }
        
        public decimal Amount
        {
            get; set;
        }
        public int ContributionID { get; set; }
        public DateTime ContributionDate { get; set; }
       
    }

    public class ContributionType
    {
        public int ContributionTypeID { get; set; }

        public string Name { get; set; }
        public string GroupType { get; set; }

        public decimal FixAmount { get; set; }
        public string Description { get; set; }
        
    }
}
