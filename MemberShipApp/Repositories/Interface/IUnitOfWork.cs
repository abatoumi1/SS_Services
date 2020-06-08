using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICountryRepository Countries { get; }
        IStateRepository States { get; }
        IRegionRepository Regions { get; }
        IPositionRepository Positions { get; }

        IMemberRepository Members { get; }
        Task<int> CommitAsync();
    }
}
