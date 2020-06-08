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
        Task<Member> GetMemberById(int id);
        Task<IEnumerable<MemberDto>> GetAllConnectMembers();
        Task<ReturnResponse> CreateMember(Member newMember);
        Task<ReturnResponse> UpdateMember(int id, Member member);
        Task<ReturnResponse> DeleteMember(int id);
    }
}
