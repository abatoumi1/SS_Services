using MemberShipApp.Extensions;
using MemberShipApp.Models;
using MemberShipApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Services
{
    public interface IContributionServices
    {
        Task<IEnumerable<ContributionMethodDto>> GetContributionMethod();
        Task<IEnumerable<ContributionDto>> SerachContributionByMemberID(ContributionFilter filte);
        Task<IEnumerable<ContributionDto>> GetAllContributions();
        Task<ContributionDto> GetContributionById(int id);
        Task<ReturnResponse> CreateContribution(ContributionDto newContribution);
        Task<ReturnResponse> UpdateContribution(int id, ContributionDto contribution);
        Task<ReturnResponse> DeleteContribution(int id);
    }
}
