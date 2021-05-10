using MemberShipApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MemberShipApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MemberShipContext _context;
        private CountryRepository _countryRepository;
        private RegionRepository _regionRepository;
        private StateRepository _stateRepository;
        private PositionRepository _positionRepository;
        private MemberRepository _memberRepository;
        private ContributionRepository _contributionRepository;
        public UnitOfWork(MemberShipContext context)
        {
            this._context = context;
        }

        public ICountryRepository Countries => _countryRepository = _countryRepository ?? new CountryRepository(_context);
   
        public IStateRepository States => _stateRepository = _stateRepository ?? new StateRepository(_context);

        public IRegionRepository Regions => _regionRepository = _regionRepository ?? new RegionRepository(_context);

        public IPositionRepository Positions => _positionRepository = _positionRepository ?? new PositionRepository(_context);

        public IMemberRepository Members => _memberRepository = _memberRepository ?? new MemberRepository(_context);
        public IContributionRepository Contributions => _contributionRepository = _contributionRepository ?? new ContributionRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
           
            _context.Dispose();
        }
    }
}