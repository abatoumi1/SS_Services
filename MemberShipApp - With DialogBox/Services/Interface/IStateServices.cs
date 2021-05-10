using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Services
{
   public interface IStateServices
    {
        Task<IEnumerable<State>> GetAllWithRegionsAsync();
        Task<IEnumerable<State>> GetWithStateByRegionIDAsync(int regionID);
       // Task<IEnumerable<State>> GetWithStateByCountryIDAsync(int countryID);
        Task<IEnumerable<StateDto>> GetAllStatesByCountryID(int countryID);
        Task<IEnumerable<State>> GetAllStates();
        Task<State> GetStateById(int id);
        Task<ReturnResponse> CreateState(State newState);
        Task<ReturnResponse> UpdateState(int id, State state);
        Task<ReturnResponse> DeleteState(int id);
    }
}
