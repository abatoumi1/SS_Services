using MemberShipApp.Extensions;
using MemberShipApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Models
{
    public class MemberView
    {
       public  PaginatedList<MemberDto> PageList { get; set; }
       public MemberDto SelectedMember { get; set; }
    }
}
