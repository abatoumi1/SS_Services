using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberShipApp.Models;
using MemberShipApp.Repositories;

namespace MemberShipApp.Services
{
    public class PositionServices : IPositionServices
    {

        private readonly IUnitOfWork _unitOfWork;
        //private ReturnResponse response = new ReturnResponse();
        public PositionServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<ReturnResponse> CreatePosition(Position newPosition)
        {
            ReturnResponse response = new ReturnResponse();
            if (PositionExists(newPosition.Name))
            {
                response.ErrorMessages = new string[] { $"{newPosition.Name} exist already." };

                return response;

            }
            await _unitOfWork.Positions.AddAsync(newPosition);
            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }

        public async Task<ReturnResponse> DeletePosition(int id)
        {
            ReturnResponse response = new ReturnResponse();
            var position = await _unitOfWork.Positions.GetByIdAsync(id);
            if (position == null)
            {
                response.ErrorMessages = new string[] { $"Position name {position.Name} not found." };
                return response;
            }

            _unitOfWork.Positions.Remove(position);
            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }

        public async Task<IEnumerable<Position>> GetAllPositions()
        {
            return await _unitOfWork.Positions.GetAllAsync();
        }

        public async Task<Position> GetPositionById(int id)
        {
            return await _unitOfWork.Positions.GetByIdAsync(id);
        }

        public async Task<ReturnResponse> UpdatePosition(int id, Position position)
        {
            ReturnResponse response = new ReturnResponse();
            var positionToBeUpdated = await GetPositionById(id);
            if (positionToBeUpdated == null)
            {
                response.ErrorMessages = new string[] { $"Position name {position.Name} not found." };
                return response;
            }

            positionToBeUpdated.Name = position.Name;
            positionToBeUpdated.Description= position.Description;
            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }

        private bool PositionExists(string name)
        {
            return _unitOfWork.Positions.AnyAs(e => e.Name == name);
        }
        private bool PositionExists(int id)
        {
            return _unitOfWork.Positions.AnyAs(e => e.PositionID == id);
        }
    }
}
