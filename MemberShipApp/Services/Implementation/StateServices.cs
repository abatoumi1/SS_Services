using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberShipApp.Models;
using MemberShipApp.Repositories;

namespace MemberShipApp.Services
{
    public class StateServices : IStateServices
    {

        private readonly IUnitOfWork _unitOfWork;
        //private ReturnResponse response = new ReturnResponse();
        public StateServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<ReturnResponse> CreateState(State newState)
        {
            ReturnResponse response = new ReturnResponse();
            if (StateExists(newState.Name))
            {
                response.ErrorMessages = new string[] { $"{newState.Name} exist already." };

                return response;

            }
            await _unitOfWork.States.AddAsync(newState);
            await _unitOfWork.CommitAsync();
            response.Success = true;

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

            return response;
        }

        public async Task<IEnumerable<State>> GetAllStates()
        {
            return await _unitOfWork.States.GetAllAsync();
        }

        public async Task<IEnumerable<State>> GetAllWithRegionsAsync()
        {
            return await _unitOfWork.States.GetAllWithRegionsAsync();
        }

        public async Task<State> GetStateById(int id)
        {
            return await _unitOfWork.States.GetByIdAsync(id);
        }

        public Task<IEnumerable<State>> GetWithStateByCountryIDAsync(int countryID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<State>> GetWithStateByRegionIDAsync(int regionID)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StateDto>> GetAllStatesByCountryID(int countryID)
        {
            
            return await  _unitOfWork.States.GetAllStatesByCountryID(countryID);
        }

        public async Task<ReturnResponse> UpdateState(int id, State state)
        {
            ReturnResponse response = new ReturnResponse();
            var stateToBeUpdated = await GetStateById(id);
            if (stateToBeUpdated == null)
            {
                response.ErrorMessages = new string[] { $"Country name {state.Name} not found." };
                return response;
            }

            stateToBeUpdated.Name = state.Name;
            stateToBeUpdated.RegionID = state.RegionID;
            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }

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
