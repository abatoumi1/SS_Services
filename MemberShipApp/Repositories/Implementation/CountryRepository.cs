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
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(MemberShipContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Country>> GetAllWithRegionsAsync()
        {
            return await MemberShipContext.Countries
                .Include(m => m.Regions)
                .ToListAsync();
        }

        public async Task<CountryDto> GetAllWithRegionsByIdAsync(int countryID)
        {
            var country= await MemberShipContext.Countries
                .Include(m => m.Regions).FirstOrDefaultAsync(s=>s.CountryID==countryID);
            var regions = country.Regions.Select(s => s.RegionID);
            var states = regions.SelectMany(id => MemberShipContext.States.Where(s => s.RegionID == id));

            return DBFacade.CountryDto(country);
        }

        private MemberShipContext MemberShipContext
        {
            get { return Context as MemberShipContext; }
        }
    }
}
