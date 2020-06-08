using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberShipApp.Models;
using MemberShipApp.Repositories;

namespace MemberShipApp.Services
{
    public class CountryServices : ICountryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        //private ReturnResponse response = new ReturnResponse();
        public CountryServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
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

            return response;
        }

        async Task<IEnumerable<Country>> ICountryServices.GetAllCountries()
        {
            return await _unitOfWork.Countries.GetAllAsync();
        }

        async Task<IEnumerable<Country>> ICountryServices.GetAllWithRegions()
        {
            return await _unitOfWork.Countries.GetAllWithRegionsAsync();
        }

        public async Task<Country> GetCountryById(int id)
        {
            return await _unitOfWork.Countries.GetByIdAsync(id);
               
        }

        public async Task<ReturnResponse> UpdateCountry(int id, Country country)
        {
            ReturnResponse response = new ReturnResponse();
            var countryToBeUpdated = await GetCountryById(id);
            if (countryToBeUpdated==null)
            {
                response.ErrorMessages = new string[] { $"Country name {country.Name} not found." };
                return response;
            }
           
            countryToBeUpdated.Name = country.Name;            
            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }

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
