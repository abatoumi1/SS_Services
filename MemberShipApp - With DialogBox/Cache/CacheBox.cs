using MemberShipApp.Extensions.DBFacade;
using MemberShipApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.Data;

namespace MemberShipApp.Cache
{
    public class CacheBox
    {
       static MemberShipContext _context ;
        public CacheBox(MemberShipContext context) 
        {
           // _context = context;
        }

        private static readonly CachedCollection<RegionDto> locations
                               = new CachedCollection<RegionDto>(
                                       () =>
                                       { 
                                           return _context.Regions.ToList().Select(s => DBFacade.RegionDto(s)).ToDictionary(l => l.RegionID, l => l);
                                       });

       
    }
}
