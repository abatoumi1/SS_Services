using MemberShipApp.Data;
using MemberShipApp.Extensions.DBFacade;
using MemberShipApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MemberShipApp.Repositories
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(MemberShipContext context)
            :base(context)
        {

        }
        public async Task<IEnumerable<State>> GetAllWithRegionsAsync()
        {
            return await MemberShipContext.States
                .Include(a => a.Region)
                 .AsNoTracking()
                 .ToListAsync();
        }

        //public async Task<IEnumerable<State>> GetWithCountryByIdAsync(int countryID)
        //{
        //    var members = await MemberShipContext.States
        //        .Include(a=>a.Region.CountryID)
        //        // .Where(s => s. == regionID)
        //         .AsNoTracking()
        //         .ToListAsync();
        //    return members.Where(a => a.Region.CountryID == countryID);
        //}

        public async Task<IEnumerable<State>> GetWithRegionByIdAsync(int regionID)
        {
            return await MemberShipContext.States
                .Where(s=>s.RegionID==regionID)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<State>> GetAllStatesByCountryID(int countryID)
        {
            var members = await MemberShipContext.States
                 .Include(a => a.Region)
                  // .Where(s => s. == regionID)
                  .AsNoTracking()
                  .ToListAsync();
            return members.Where(a => a.Region.CountryID == countryID);
        }

        private MemberShipContext MemberShipContext
        {
            get { return Context as MemberShipContext; }
        }
    }
}
