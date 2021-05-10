using MemberShipApp.Models;
using MemberShipApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Services
{
    public interface IMemberServices
    {
        Task<IEnumerable<MemberDto>> GetAllMembers();
        Task<MemberDto> GetMemberById(int id);
        Task<IEnumerable<MemberDto>> GetAllConnectMembers();
        Task<ReturnResponse> CreateMember(MemberDto newMember);
        Task<ReturnResponse> UpdateMember(int id, MemberDto member);
        Task<ReturnResponse> DeleteMember(int id);
        Task<MemberDetails> GetMemberDetails(int id);
        Task<IEnumerable<MemberListing>> GetAllMembersByCountryAsync(int countryID);
        Task<IEnumerable<MemberYearlyContribution>> GetMemberYearlyContributionByCountryAsync(int countryID, int year);
    }
}
