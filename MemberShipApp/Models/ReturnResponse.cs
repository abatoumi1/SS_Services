using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class ReturnResponse
    {
        public  string[] ErrorMessages { get; set; }
        public  bool Success { get; set; }

        public  bool IsSuccessful()
        {
            return Success;
        }

        public  T Data<T>( T content)
        {
            return content;
        }
    }
}
