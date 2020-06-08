using MemberShipApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.Data;

namespace MemberShipApp.Repositories
{
    public class MemberRepository : Repository<Member> , IMemberRepository
    {
        public MemberRepository(MemberShipContext context)
            : base(context) { }

        public async Task<IEnumerable<Member>> GetAllWithConnectAsync()
        {
            return await MemberShipContext.Members
                .Include(m => m.State)
                    .ThenInclude(v=>v.Region)
                .Include(b=> b.Position)
                .AsNoTracking()
                .ToListAsync();
        }

        private MemberShipContext MemberShipContext
        {
            get { return Context as MemberShipContext; }
        }
    }
}
