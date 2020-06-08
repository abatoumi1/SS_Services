using MemberShipApp.Extensions.DBFacade;
using MemberShipApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.Data;

namespace MemberShipApp.Repositories
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(MemberShipContext context)
            :base(context)
        {

        }
        public Task<IEnumerable<State>> GetAllWithRegionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<State>> GetWithCountryByIdAsync(int countryID)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<State>> GetWithRegionByIdAsync(int regionID)
        {
            return await MemberShipContext.States
                .Where(s=>s.RegionID==regionID)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<StateDto>> GetAllStatesByCountryID(int countryID)
        {
            var country = await MemberShipContext.Countries
                .Include(m => m.Regions).FirstOrDefaultAsync(s => s.CountryID == countryID);
            var regions = country.Regions.Select(s => s.RegionID);
            var states = regions.SelectMany(id => MemberShipContext.States.Where(s => s.RegionID == id));

            return states.Select(s=>DBFacade.StateDto(s));
        }

        private MemberShipContext MemberShipContext
        {
            get { return Context as MemberShipContext; }
        }
    }
}
