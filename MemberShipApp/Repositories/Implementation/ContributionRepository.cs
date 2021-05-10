using MemberShipApp.Data;
using MemberShipApp.Extensions;

using MemberShipApp.Models;
using MemberShipApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MemberShipApp.Repositories
{
    public class ContributionRepository: Repository<Contribution>, IContributionRepository
    {
        public ContributionRepository(MemberShipContext context)
            : base(context)
        { }
        public async Task<IEnumerable<ContributionMethod>> GetContributionMethod()
        {
            return await MemberShipContext.ContributionMethods.ToListAsync();
        }
        public  async Task<IEnumerable<Contribution>> SerachContributionByMemberID(ContributionFilter filter)
        {
            if (filter.MemberID.HasValue)
            {
                var contributions = await MemberShipContext.Contributions
                .Where(a => a.MemberID == filter.MemberID.Value)
                .ToListAsync();
                if (filter.StartDate.HasValue && !filter.EndDate.HasValue)
                {
                    Date endDate = filter.StartDate.Value.AddDays(2);
                    contributions = contributions.Where(a => a.ContributionDate.Date >= filter.StartDate.Value && a.ContributionDate.Date <= endDate).ToList();
                }
               
                else if(filter.StartDate.HasValue && filter.EndDate.HasValue)
                {
                    contributions = contributions.Where(a => a.ContributionDate.Date >= filter.StartDate.Value && a.ContributionDate.Date <= filter.EndDate.Value).ToList();
                }
                return contributions;
            }
            else
            {
                if (filter.StartDate.HasValue && !filter.EndDate.HasValue)
                {
                    Date endDate = filter.StartDate.Value.AddDays(2);
                    var contributions = await MemberShipContext.Contributions.Where(a => a.ContributionDate.Date >= filter.StartDate.Value && a.ContributionDate.Date <= endDate.Value).ToListAsync();
                }

                else if (filter.StartDate.HasValue && filter.EndDate.HasValue)
                {
                    var contributions = await MemberShipContext.Contributions.Where(a => a.ContributionDate.Date >= filter.StartDate.Value && a.ContributionDate.Date <= filter.EndDate.Value).ToListAsync();
                }
                return new List<Contribution>();
            }
            
        }
        private MemberShipContext MemberShipContext
        {
            get { return Context as MemberShipContext; }
        }

    }
}
