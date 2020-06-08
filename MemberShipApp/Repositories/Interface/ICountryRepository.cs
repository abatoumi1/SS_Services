using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<IEnumerable<Country>> GetAllWithRegionsAsync();
       
        
    }
}
