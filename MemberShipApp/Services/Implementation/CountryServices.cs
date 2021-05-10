using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberShipApp.Extensions;
using MemberShipApp.Extensions.DBFacade;
using MemberShipApp.Models;
using MemberShipApp.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace MemberShipApp.Services
{
    public class CountryServices : ICountryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        //private ReturnResponse response = new ReturnResponse();
        public CountryServices(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            this._unitOfWork = unitOfWork;
            this._cache = cache;
        }


        public async Task<ReturnResponse> CreateCountry(Country newCountry)
        {
            ReturnResponse response = new ReturnResponse();
            if (CountryExists(newCountry.Name))
            {
                 response.ErrorMessages =new string[] {$"{newCountry.Name} exist already." };

                return response;

            }
            await _unitOfWork.Countries.AddAsync(newCountry);
            await _unitOfWork.CommitAsync();
            response.Success = true;
            ActionCache.StateRegionCountryRemoval(_cache);
            return response;
        }

        public async Task<ReturnResponse> DeleteCountry(int id)
        {
            ReturnResponse response = new ReturnResponse();
            var country = await _unitOfWork.Countries.GetByIdAsync(id);
            if (country == null)
            {
                response.ErrorMessages = new string[] { $"Country name {country.Name} not found." };
                return response;
            }
           
            _unitOfWork.Countries.Remove(country);
            await _unitOfWork.CommitAsync();
            response.Success = true;
            ActionCache.StateRegionCountryRemoval(_cache);
            return response;
        }

        public async Task<IEnumerable<CountryDto>> GetAllCountries()
        {
            List<CountryDto> countries = new List<CountryDto>();
            if (!_cache.TryGetValue(CacheKeys.Countries, out countries))
            {
                var countrys = await _unitOfWork.Countries.GetAllAsync();
                //countries = countrys.Select(s => DBFacade.CountryTuplate(s)).ToList();
                _cache.Set(CacheKeys.Countries, countrys.Select(s => DBFacade.CountryDto(s)).ToList());
            }
            return _cache.Get(CacheKeys.Countries) as List<CountryDto>;
            //var countries= await _unitOfWork.Countries.GetAllAsync();
            //return countries.Select(s => DBFacade.CountryDto(s));
        }

        public async Task<IEnumerable<CountryTuplate>> GetAllCountryWithConnect()
        {
            List<CountryTuplate> countries = new List<CountryTuplate>();
            if(!_cache.TryGetValue(CacheKeys.CountryTuplates, out countries))
            {
                var countrys = await _unitOfWork.Countries.GetAllWithRegionsAndStatesAsync();
                //countries = countrys.Select(s => DBFacade.CountryTuplate(s)).ToList();
                _cache.Set(CacheKeys.CountryTuplates, countrys.Select(s => DBFacade.CountryTuplate(s)).ToList());
            }            
           return _cache.Get(CacheKeys.CountryTuplates) as List<CountryTuplate>;
        }

        public async Task<CountryTuplate> GetAllCountryWithConnect(int countryID)
        {
            var country = await GetAllCountryWithConnect();
            return country.FirstOrDefault(a => a.CountryID == countryID);
            //var country = await _unitOfWork.Countries.GetAllWithRegionsByIdAsync(countryID);
            //return  DBFacade.CountryTuplate(country);
        }

        public async Task<Country> GetCountryById(int id)
        {
            return await _unitOfWork.Countries.GetByIdAsync(id);
               
        }

        public async Task<ReturnResponse> UpdateCountry(int id, Country country)
        {
            ReturnResponse response = new ReturnResponse();
            var countryToBeUpdated = await _unitOfWork.Countries.GetAllWithRegionsByIdAsync(id);
            if (countryToBeUpdated==null)
            {
                response.ErrorMessages = new string[] { $"Country name {country.Name} not found." };
                return response;
            }
            countryToBeUpdated.CountryID = id;
            countryToBeUpdated.Code = country.Code;
            countryToBeUpdated.Name = country.Name;            
            await _unitOfWork.CommitAsync();
            response.Success = true;
            ActionCache.StateRegionCountryRemoval(_cache);
            return response;
        }
        //private void RemoveCache()
        //{
        //    _cache.Remove("Countries");
        //    _cache.Remove("States");
        //    _cache.Remove("Regions");
        //    _cache.Remove("CountryTuplate");
        //}
        private bool CountryExists(string name)
        {
            return  _unitOfWork.Countries.AnyAs(e => e.Name == name);
        }
        private bool CountryExists(int id)
        {
            return _unitOfWork.Countries.AnyAs(e => e.CountryID == id);
        }
    }
}
