using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Services
{
   public interface IStateServices
    {
        //Task<IEnumerable<State>> GetAllWithRegionsAsync();
        Task<IEnumerable<StateDto>> GetWithStateByRegionIDAsync(int regionID);
       // Task<IEnumerable<State>> GetWithStateByCountryIDAsync(int countryID);
        Task<IEnumerable<StateDto>> GetAllStatesByCountryID(int countryID);
        Task<IEnumerable<StateDto>> GetAllStates();
        Task<StateDto> GetStateById(int id);
        Task<ReturnResponse> CreateState(StateDto newState);
        Task<ReturnResponse> UpdateState(int id, StateDto state);
        Task<ReturnResponse> DeleteState(int id);
    }
}
