using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Extensions
{
    public static class GenerateYears
    {
        public static List<int> PopulateYears()
        {
            List<int> lst = new List<int>();
            for (int min = DateTime.Today.Year - 10; min <= DateTime.Today.Year; min++)
            {
                lst.Add(min);
            }
            return lst;

        }
    }
}
