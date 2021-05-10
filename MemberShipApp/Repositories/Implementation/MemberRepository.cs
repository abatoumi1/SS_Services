using MemberShipApp.Extensions.ExecuteStoreProc;
using MemberShipApp.Models;
using MemberShipApp.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

using MemberShipApp.Extensions;
using MemberShipApp.Data;

namespace MemberShipApp.Repositories
{
    public class MemberRepository : Repository<Member> , IMemberRepository
    {
        //MemberShipContext context;
        public MemberRepository(MemberShipContext context)
            : base(context) { }

        public async Task<IEnumerable<Member>> GetAllWithConnectAsync()
        {
            return await MemberShipContext.Members
                .Include(m => m.State)
                    .ThenInclude(v=>v.Region)
                        .ThenInclude(v1=>v1.Country)
                .Include(b=> b.Position)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Member>> GetAllByCountryAsync(int countryID)
        {
            return await MemberShipContext.Members
                .Include(m => m.State)
                    .ThenInclude(v => v.Region)
                        .ThenInclude(v1 => v1.Country)
                .Include(b => b.Position)
                .AsNoTracking().Where(a => a.State.Region.CountryID== countryID).ToListAsync();
        }

        public async Task<Member> GetAllMemberDetails(int id)
        {
            return await MemberShipContext.Members
                .Include(m => m.Contributions)
                .Include(m => m.State)
                    .ThenInclude(v => v.Region)
                        .ThenInclude(v1 => v1.Country)
                .Include(b => b.Position)
                .AsNoTracking().FirstOrDefaultAsync(a => a.MemberID == id);
        }

        public async Task<IEnumerable<MemberYearlyContribution>> GetMemberYearlyContributionByCountryAsync(int countryID, int year)
        {
            
            //List<MemberYearlyContribution> myTypeList = new List<MemberYearlyContribution>();
           
                DbCommand cmd = MemberShipContext.LoadStoredProc("MemberByYearlyContribution")
                .WithSqlParam("countryID", countryID)
                .WithSqlParam("year", year);
                return await StoreProc.ExecuteStoredProc<MemberYearlyContribution>(cmd);
       
        }
        private MemberShipContext MemberShipContext
        {
            get { return Context as MemberShipContext; }
        }
    }
}
