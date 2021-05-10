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
    public class StateServices : IStateServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public StateServices(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._cache = cache;
        }
        public async Task<ReturnResponse> CreateState(StateDto newState)
        {
            ReturnResponse response = new ReturnResponse();
            if (StateExists(newState.Name))
            {
                response.ErrorMessages = new string[] { $"{newState.Name} exist already." };

                return response;

            }
            var state = new State
            {
                Name = newState.Name,
                RegionID =newState.RegionID,
                Code=newState.Code
            };
            await _unitOfWork.States.AddAsync(state);
            await _unitOfWork.CommitAsync();
            response.Success = true;
            ActionCache.StateRegionCountryRemoval(_cache);
            return response;
        }

        public async Task<ReturnResponse> DeleteState(int id)
        {
            ReturnResponse response = new ReturnResponse();
            var state = await _unitOfWork.States.GetByIdAsync(id);
            if (state == null)
            {
                response.ErrorMessages = new string[] { $"State name {state.Name} not found." };
                return response;
            }

            _unitOfWork.States.Remove(state);
            await _unitOfWork.CommitAsync();
            response.Success = true;
            ActionCache.StateRegionCountryRemoval(_cache);
            return response;
        }

        public async Task<IEnumerable<StateDto>> GetAllStates()
        {
            _ = new List<StateDto>();
            if (!_cache.TryGetValue(CacheKeys.States, out _))
            {
                var state = await _unitOfWork.States.GetAllWithRegionsAsync();
                //countries = countrys.Select(s => DBFacade.CountryTuplate(s)).ToList();
                _cache.Set(CacheKeys.States, _mapper.Map<IEnumerable<State>, IEnumerable<StateDto>>(state));
            }
            return _cache.Get(CacheKeys.States) as List<StateDto>;

            //return await _unitOfWork.States.GetAllWithRegionsAsync();
        }

        //public async Task<IEnumerable<State>> GetAllWithRegionsAsync()
        //{
        //    return await _unitOfWork.States.GetAllWithRegionsAsync();
        //}

        public async Task<StateDto> GetStateById(int id)
        {
            var state= await _unitOfWork.States.GetByIdAsync(id);
            return _mapper.Map<State, StateDto>(state);
        }

        public Task<IEnumerable<StateDto>> GetWithStateByCountryIDAsync(int countryID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StateDto>> GetWithStateByRegionIDAsync(int regionID)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StateDto>> GetAllStatesByCountryID(int countryID)
        {
            
           var states= await  _unitOfWork.States.GetAllStatesByCountryID(countryID);
            return _mapper.Map<IEnumerable<State>, IEnumerable<StateDto>>(states);
        }

        public async Task<ReturnResponse> UpdateState(int id, StateDto state)
        {
            ReturnResponse response = new ReturnResponse();
            var stateToBeUpdated = await _unitOfWork.States.GetByIdAsync(id);
            if (stateToBeUpdated == null)
            {
                response.ErrorMessages = new string[] { $"Country name {state.Name} not found." };
                return response;
            }

            stateToBeUpdated.StateID = id;
            stateToBeUpdated.Name = state.Name;
            stateToBeUpdated.Code = state.Code;
            stateToBeUpdated.RegionID = state.RegionID;
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
        private bool StateExists(string name)
        {
            return _unitOfWork.States.AnyAs(e => e.Name == name);
        }
        private bool StateExists(int id)
        {
            return _unitOfWork.States.AnyAs(e => e.StateID == id);
        }
    }
}
