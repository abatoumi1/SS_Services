using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Extensions
{
    public class ContributionFilter
    {

        /// <summary>
        /// Filter by member ID
        /// </summary>
        public int? MemberID { get; set; }
        /// <summary>
        /// Filter for a certain day only
        /// </summary>
        public Date? StartDate { get; set; }

            /// <summary>
            /// Filter for a certain day only
            /// </summary>
            public Date? EndDate { get; set; }

            /// <summary>
            /// Specifies if the filter date is limited to an endDate.
            /// If true the Filter Date is limited to the day that is passed.
            /// If false the Filter Date is unlimited.
            /// If not supplied the Filter Date is limited.
            /// </summary>
            
            public bool? FilterDateHasEnd { get; set; }

        }

    }
