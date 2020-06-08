using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Repositories
{
   public interface IMemberRepository : IRepository<Member>
    {
        Task<IEnumerable<Member>> GetAllWithConnectAsync();
    }
}
