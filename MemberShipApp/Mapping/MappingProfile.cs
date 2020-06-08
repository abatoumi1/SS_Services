using MemberShipApp.Models;
using MemberShipApp.Models.DTO;
using AutoMapper;

namespace MemberShipApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Member, MemberDto>();
            //CreateMap<Artist, ArtistResource>();

            // Resource to Domain
            CreateMap<MemberDto, Member>();
           // CreateMap<ArtistResource, Artist>();
        }
    }
}
