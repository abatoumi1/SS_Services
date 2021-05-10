using AutoMapper;
using MemberShipApp.Extensions;
using MemberShipApp.Models;
using MemberShipApp.Models.DTO;
using MemberShipApp.Repositories;
using MemberShipApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Services
{
    public class ContributionServices : IContributionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContributionServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        async Task<ReturnResponse> IContributionServices.CreateContribution(ContributionDto newContribution)
        {
            ReturnResponse response = new ReturnResponse();
            if (!MemberExists(newContribution.MemberID))
            {
                response.ErrorMessages = new string[] { $"MemberID {newContribution.MemberID} not found." };
                return response;
            }
            var contribution = new Contribution
            {
                MemberID=newContribution.MemberID,
                ContributionMethodID = newContribution.ContributionMethodID,
                Amount = newContribution.Amount,
                ContributionDate= DateTime.Now
            };
            await _unitOfWork.Contributions.AddAsync(contribution);
            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }

        Task<ReturnResponse> IContributionServices.DeleteContribution(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ContributionDto>> IContributionServices.GetAllContributions()
        {
            throw new NotImplementedException();
        }

        Task<ContributionDto> IContributionServices.GetContributionById(int id)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<ContributionDto>> IContributionServices.SerachContributionByMemberID(ContributionFilter filter)
        {
            var contributions = await _unitOfWork.Contributions.SerachContributionByMemberID(filter);
            return _mapper.Map<IEnumerable<Contribution>, IEnumerable<ContributionDto>>(contributions); 
        }

        async Task<ReturnResponse> IContributionServices.UpdateContribution(int id, ContributionDto contribution)
        {
            ReturnResponse response = new ReturnResponse();
            var contributionToBeUpdated = await _unitOfWork.Contributions.GetByIdAsync(id);
            if (contributionToBeUpdated == null)
            {
                response.ErrorMessages = new string[] { $"This contribution ID   not found." };
                return response;
            }

            contributionToBeUpdated.ContributionID = contribution.ContributionID;
            contributionToBeUpdated.ContributionMethodID = contribution.ContributionMethodID;
            contributionToBeUpdated.ContributionDate = contribution.ContributionDate;
            contributionToBeUpdated.Amount = contribution.Amount;

            await _unitOfWork.CommitAsync();
            response.Success = true;

            return response;
        }
        public async Task<IEnumerable<ContributionMethodDto>> GetContributionMethod()
        {
            var methods = await _unitOfWork.Contributions.GetContributionMethod();
            return _mapper.Map<IEnumerable<ContributionMethod>, IEnumerable<ContributionMethodDto>>(methods);
        }
        private bool MemberExists(int id)
        {
            return _unitOfWork.States.AnyAs(e => e.StateID == id);
        }
    }
}
