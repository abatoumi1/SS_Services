using MemberShipApp.Models;
using MemberShipApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Repositories
{
   public interface IMemberRepository : IRepository<Member>
    {
        Task<IEnumerable<Member>> GetAllWithConnectAsync();
        Task<Member> GetAllMemberDetails(int id);
        Task<IEnumerable<Member>> GetAllByCountryAsync(int countryID);
        Task<IEnumerable<MemberYearlyContribution>> GetMemberYearlyContributionByCountryAsync(int countryID, int year);
    }
}
