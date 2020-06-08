using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Repositories
{
    public interface IRegionRepository : IRepository<Region>
    {      
        Task<IEnumerable<Region>> GetWithRegionByIdAsync(int country);
        Task<IEnumerable<Region>> GetWithStatesAsync();
        Task<Region> GetStatesByIdAsync(int regionID);



    }
}
