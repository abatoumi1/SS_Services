using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Services
{
    public interface IPositionServices
    {
        Task<IEnumerable<Position>> GetAllPositions();
        Task<Position> GetPositionById(int id);
        Task<ReturnResponse> CreatePosition(Position newPosition);
        Task<ReturnResponse> UpdatePosition(int id, Position position);
        Task<ReturnResponse> DeletePosition(int id);
    }
}
