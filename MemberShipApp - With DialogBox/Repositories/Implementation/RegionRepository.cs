using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberShipApp.Extensions.DBFacade;
using MemberShipApp.Models;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;

namespace MemberShipApp.Repositories
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(MemberShipContext context)
            :base(context)
        {

        }

        async Task<Region> IRegionRepository.GetStatesByIdAsync(int regionID)
        {
            return await MemberShipContext.Regions.Include(a => a.States).FirstOrDefaultAsync(a => a.RegionID == regionID);
            //return DBFacade.RegionDto(region);
        }
        async Task<IEnumerable<Region>> IRegionRepository.GetWithRegionByIdAsync(int countryID)
        {
            return await MemberShipContext.Regions.Include(a => a.States).Where(a => a.CountryID == countryID).ToListAsync();
            //return regions.Select(s => DBFacade.RegionDto(s));
        }

        async Task<IEnumerable<Region>> IRegionRepository.GetWithStatesAsync()
        {
            return await MemberShipContext.Regions.Include(a=>a.States).ToListAsync();
            //return regions.Select(s => DBFacade.RegionDto(s));
        }
        private MemberShipContext MemberShipContext
        {
            get { return Context as MemberShipContext; }
        }
    }
}
