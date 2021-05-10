using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Repositories
{
    public interface IStateRepository : IRepository<State>
    {
        Task<IEnumerable<State>> GetAllWithRegionsAsync();
        Task<IEnumerable<State>> GetWithRegionByIdAsync(int regionID);
        //Task<IEnumerable<State>> GetWithCountryByIdAsync(int countryID);
        Task<IEnumerable<State>> GetAllStatesByCountryID(int countryID);


    }
}
