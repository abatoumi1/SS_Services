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
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(MemberShipContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Country>> GetAllWithRegionsAndStatesAsync()
        {
            return await MemberShipContext.Countries
                .Include(m => m.Regions)
                    .ThenInclude(v=>v.States)
                .ToListAsync();
        }

        public async Task<Country> GetAllWithRegionsByIdAsync(int countryID)
        {
            return await MemberShipContext.Countries
                            .Include(m => m.Regions)
                                .ThenInclude(v => v.States)
                            .FirstOrDefaultAsync(s=>s.CountryID==countryID);
            

           
        }

        private MemberShipContext MemberShipContext
        {
            get { return Context as MemberShipContext; }
        }
    }
}
