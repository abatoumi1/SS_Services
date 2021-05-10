using MemberShipApp.Extensions;

using MemberShipApp.Models;
using MemberShipApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Repositories
{
    public interface IContributionRepository : IRepository<Contribution>
    {
        Task<IEnumerable<ContributionMethod>> GetContributionMethod();
        Task<IEnumerable<Contribution>> SerachContributionByMemberID(ContributionFilter filte);
    }
}
