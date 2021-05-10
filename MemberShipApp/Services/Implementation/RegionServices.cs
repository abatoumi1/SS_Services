using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MemberShipApp.Extensions;
using MemberShipApp.Extensions.DBFacade;
using MemberShipApp.Models;
using MemberShipApp.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace MemberShipApp.Services
{
    public class RegionServices : IRegionServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public RegionServices(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._cache = cache;
        }
        public async Task<ReturnResponse> CreateRegion(RegionDto newRegion)
        {
            ReturnResponse response = new ReturnResponse();
            if (RegionExists(newRegion.Name))
            {
                response.ErrorMessages = new string[] { $"{newRegion.Name} exist already." };
                return response;
            }

            var country = await _unitOfWork.Countries.GetByIdAsync(newRegion.CountryID);
            var region = new Region
            {
                Name = newRegion.Name,
                Description = newRegion.Description
            };
            region.Country = country;
            foreach(var id in newRegion.StateIDs)
            {
                var state =await _unitOfWork.States.GetByIdAsync(id.ID);
                region.States.Add(state);
            }

            await _unitOfWork.Regions.AddAsync(region);
            await _unitOfWork.CommitAsync();
            response.Success = true;
            ActionCache.StateRegionCountryRemoval(_cache);
            return response;
        }

        public async Task<ReturnResponse> DeleteRegion(int id)
        {
            ReturnResponse response = new ReturnResponse();
            var region = await _unitOfWork.Regions.GetByIdAsync(id);
            if (region == null)
            {
                response.ErrorMessages = new string[] { $"Region name {region.Name} not found." };
                return response;
            }

            _unitOfWork.Regions.Remove(region);
            await _unitOfWork.CommitAsync();
            response.Success = true;
            ActionCache.StateRegionCountryRemoval(_cache);
            return response;
        }

        public async Task<IEnumerable<RegionDto>> GetAllRegions()
        {
            List<RegionDto> regions = new List<RegionDto>();
            if (!_cache.TryGetValue(CacheKeys.Regions, out regions))
            {
                var region = await _unitOfWork.Regions.GetWithStatesAsync();
                //countries = countrys.Select(s => DBFacade.CountryTuplate(s)).ToList();
                _cache.Set(CacheKeys.Regions, region.Select(s => DBFacade.RegionDto(s)).ToList());
            }
            return _cache.Get(CacheKeys.Regions) as List<RegionDto>;
            //var regions= await _unitOfWork.Regions.GetAllAsync();
            //return regions.Select(s => DBFacade.RegionDto(s));
        }

        public async  Task<IEnumerable<RegionDto>> GetAllRegionByCountryID(int countryID)
        {
            var regions = await GetAllRegions();
            return regions.Where(a => a.CountryID == countryID);
            //var regions = await _unitOfWork.Regions.GetWithRegionByIdAsync(countryID);
            //return regions.Select(s => DBFacade.RegionDto(s));
            //return _mapper.Map<IEnumerable<Region>, IEnumerable<RegionDto>>(regions); ;

        }

        public async Task<RegionDto> GetRegionById(int id)
        {
           var region= await _unitOfWork.Regions.GetByIdAsync(id);
            return DBFacade.RegionDto(region);
        }

        public async Task<RegionDto> GetRegionWithStatesById(int id)
        {
          var region = await _unitOfWork.Regions.GetStatesByIdAsync(id);
            return DBFacade.RegionDto(region);
        }

        public async Task<IEnumerable<RegionDto>> GetWithStatesAsync()
        {
           var regions = await _unitOfWork.Regions.GetWithStatesAsync();
            return regions.Select(s => DBFacade.RegionDto(s));
        }

        public async Task<ReturnResponse> UpdateRegion(int id, RegionDto region)
        {
            ReturnResponse response = new ReturnResponse();
            var regionToBeUpdated = await _unitOfWork.Regions.GetStatesByIdAsync(id);
            if (regionToBeUpdated == null)
            {
                response.ErrorMessages = new string[] { $"Region name {region.Name} not found." };
                return response;
            }
            regionToBeUpdated.States.Clear();
            foreach (var sid in region.StateIDs)
            {
                var state = await _unitOfWork.States.GetByIdAsync(sid.ID);
                regionToBeUpdated.States.Add(state);
            }

            regionToBeUpdated.Country = await _unitOfWork.Countries.GetByIdAsync(region.CountryID);
            regionToBeUpdated.RegionID = id;
            regionToBeUpdated.Name = region.Name;
            regionToBeUpdated.CountryID = region.CountryID;
            regionToBeUpdated.Description = region.Description;
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
        private bool RegionExists(string name)
        {
            return _unitOfWork.Regions.AnyAs(e => e.Name == name);
        }
        private bool RegionExists(int id)
        {
            return _unitOfWork.Regions.AnyAs(e => e.CountryID == id);
        }
    }
}
