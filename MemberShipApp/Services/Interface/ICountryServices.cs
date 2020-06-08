using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Services
{
    public interface ICountryServices
    {
        Task<IEnumerable<Country>> GetAllWithRegions();
        Task<IEnumerable<Country>> GetAllCountries();
        Task<Country> GetCountryById(int id);
        Task<ReturnResponse> CreateCountry(Country newCountry);
        Task<ReturnResponse> UpdateCountry(int id, Country country);
        Task<ReturnResponse> DeleteCountry(int id);
    }
}
