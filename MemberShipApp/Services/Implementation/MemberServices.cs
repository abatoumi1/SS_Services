using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MemberShipApp.Models;
using MemberShipApp.Models.DTO;
using MemberShipApp.Repositories;

namespace MemberShipApp.Services
{
    public class MemberServices : IMemberServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MemberServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<ReturnResponse> CreateMember(Member newMember)
        {
            ReturnResponse response = new ReturnResponse();
            if (MemberExists(newMember))
            {
                response.ErrorMessages = new string[] { $"{newMember.FirstName} {newMember.LastName} exist already." };

                return response;
            }
            await _unitOfWork.Members.AddAsync(newMember);
            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }

        public async Task<ReturnResponse> DeleteMember(int id)
        {
            ReturnResponse response = new ReturnResponse();
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null)
            {
                response.ErrorMessages = new string[] { $"Member  with name {member.FirstName} {member.LastName} is not found." };
                return response;
            }

            _unitOfWork.Members.Remove(member);
            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }

        public async Task<IEnumerable<MemberDto>> GetAllMembers()
        {
            var members = await _unitOfWork.Members.GetAllAsync();

            return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberDto>>(members);
        }
        public async Task<IEnumerable<MemberDto>> GetAllConnectMembers()
        {
            var members = await _unitOfWork.Members.GetAllWithConnectAsync();
            return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberDto>>(members);
        }
        public async Task<Member> GetMemberById(int id)
        {
            return await _unitOfWork.Members.GetByIdAsync(id);
        }

        public async Task<ReturnResponse> UpdateMember(int id, Member member)
        {
            ReturnResponse response = new ReturnResponse();
            var memberToBeUpdated = await GetMemberById(id);
            if (memberToBeUpdated == null)
            {
                response.ErrorMessages = new string[] { $"Member name {member.FirstName} {member.LastName} not found." };
                return response;
            }

            memberToBeUpdated.FirstName = member.FirstName;
            memberToBeUpdated.LastName = member.LastName;
            memberToBeUpdated.Code = member.Code;
            memberToBeUpdated.Email = member.Email;
            memberToBeUpdated.EndDate = member.EndDate;
           
            memberToBeUpdated.Phone = member.Phone;
            memberToBeUpdated.StartDate = member.StartDate;
            memberToBeUpdated.PositionID = member.PositionID;
            memberToBeUpdated.StateID = member.StateID;

            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }

        private bool MemberExists(Member meb)
        {
            return _unitOfWork.Members.AnyAs(e => e.FirstName == meb.FirstName && e.LastName==meb.LastName && e.Email==meb.Email);
        }
        
    }
}
