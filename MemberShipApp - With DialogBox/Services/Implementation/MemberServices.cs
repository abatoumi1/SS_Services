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
        public async Task<ReturnResponse> CreateMember(MemberDto newMember)
        {
            ReturnResponse response = new ReturnResponse();
            if (MemberExists(newMember))
            {
                response.ErrorMessages = new string[] { $"{newMember.FirstName} {newMember.LastName} exist already." };

                return response;
            }

            Member member = new Member
            {
                FirstName = newMember.FirstName,
                LastName = newMember.LastName,
                Code = newMember.Code,
                Email = newMember.Email,
                //EndDate = newMember.EndDate,
                Phone = newMember.Phone,
                StartDate = DateTime.Now,
                PositionID = newMember.PositionID,
                StateID = newMember.StateID
            };

            await _unitOfWork.Members.AddAsync(member);
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
            member.EndDate = DateTime.Now;
           // _unitOfWork.Members.Remove(member);
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

        public  async Task<IEnumerable<MemberListing>> GetAllMembersByCountryAsync(int countryID)
        {
            var member = await _unitOfWork.Members.GetAllByCountryAsync(countryID);
            return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberListing>>(member);
        }

        public async Task<IEnumerable<MemberYearlyContribution>> GetMemberYearlyContributionByCountryAsync(int countryID, int year)
        {
           return await _unitOfWork.Members.GetMemberYearlyContributionByCountryAsync(countryID, year);
            
        }

        public async Task<MemberDto> GetMemberById(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            return _mapper.Map<Member, MemberDto>(member);
        }

        public async Task<MemberDetails> GetMemberDetails(int id)
        {
            var member = await _unitOfWork.Members.GetAllMemberDetails(id);
            return _mapper.Map<Member, MemberDetails>(member);
        }

        public async Task<ReturnResponse> UpdateMember(int id, MemberDto member)
        {
            ReturnResponse response = new ReturnResponse();
            var memberToBeUpdated = await _unitOfWork.Members.GetByIdAsync(id);
            if (memberToBeUpdated == null)
            {
                response.ErrorMessages = new string[] { $"Member name {member.FirstName} {member.LastName} not found." };
                return response;
            }

            memberToBeUpdated.FirstName = member.FirstName;
            memberToBeUpdated.LastName = member.LastName;
            memberToBeUpdated.Code = member.Code;
            memberToBeUpdated.Email = member.Email;
           // memberToBeUpdated.EndDate = member.EndDate;
           
            memberToBeUpdated.Phone = member.Phone;
            //memberToBeUpdated.StartDate = member.StartDate;
            memberToBeUpdated.PositionID = member.PositionID;
            memberToBeUpdated.StateID = member.StateID;

            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }

        private bool MemberExists(MemberDto meb)
        {
            return _unitOfWork.Members.AnyAs(e => e.FirstName == meb.FirstName && e.LastName==meb.LastName && e.Email==meb.Email);
        }
        
    }
}
