using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.Data;

namespace MemberShipApp.Repositories
{
    public class PositionRepository: Repository<Position>, IPositionRepository
    {
        public PositionRepository(MemberShipContext context)
            : base(context) { }
    }
}
