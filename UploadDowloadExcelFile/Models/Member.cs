using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadDowloadExcelFile.Models
{
   
        public enum MemberLoginStatus
        {
            None = 0,
            Submitted = 1,
            Approved = 2,
            Rejected = 3
        }

        public enum MemberLoginRole
        {
            Member = 0,
            Executive = 1,
            Admin = 2
        }
        public class Member
        {
            public int MemberID { get; set; }

            public string Code { get; set; }
          
            public bool IsActive { get; set; }
            
            public string FirstName { get; set; }

            
            public string LastName { get; set; }

           
            public string Phone { get; set; }

           
            public string Email { get; set; }

           
            public DateTime StartDate { get; set; }

            
            public MemberLoginStatus Status { get; set; }
            public MemberLoginRole Role { get; set; }
            public string OwnerID { get; set; }
           
        }
}
