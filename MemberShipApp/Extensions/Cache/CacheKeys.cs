using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Extensions
{
    public static class CacheKeys
    {
        public static string States { get { return "_States"; } }
        public static string Countries { get { return "_Countries"; } }
        public static string Regions { get { return "_Regions"; } }
        public static string CountryTuplates { get { return "_CountryTuplates"; } }
        public static string Positions { get { return "_Positions"; } }
       
    }
}
