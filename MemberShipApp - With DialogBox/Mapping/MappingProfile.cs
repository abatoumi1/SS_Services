using MemberShipApp.Models;
using MemberShipApp.Models.DTO;
using AutoMapper;
using System.Linq;

namespace MemberShipApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Member, MemberDto>()
                .ForMember(dest => dest.StateID, o => o.MapFrom(src => src.StateID))
               // .ForMember(dest => dest.StateID, o => o.MapFrom(src => src.State.Region.CountryID))
               // .ForMember(dest => dest.StateID, o => o.MapFrom(src => src.State.RegionID))
                .ForMember(dest => dest.PositionID, o => o.MapFrom(src => src.PositionID));
            CreateMap<Contribution, ContributionDto>();
            CreateMap<ContributionMethod, ContributionMethodDto>();
            CreateMap<Member, MemberDetails>()
               .ForMember(dest => dest.StateName, o => o.MapFrom(src => src.State.Name))
                .ForMember(dest => dest.CountryName, o => o.MapFrom(src => src.State.Region.Country.Name))
                .ForMember(dest => dest.RegionName, o => o.MapFrom(src => src.State.Region.Name))
               .ForMember(dest => dest.CountryID, o => o.MapFrom(src => src.State.Region.Country.CountryID))
               .ForMember(dest => dest.RegionID, o => o.MapFrom(src => src.State.Region.RegionID))
               .ForMember(dest => dest.PositionName, o => o.MapFrom(src => src.Position.Name))
               .ForMember(dest => dest.YearlyContribution, o => o.MapFrom(src => src.Contributions.Sum(s=>s.Amount)));

            CreateMap<Member, MemberListing>()
               .ForMember(dest => dest.CountryID, o => o.MapFrom(src => src.State.Region.Country.CountryID))
               .ForMember(dest => dest.RegionID, o => o.MapFrom(src => src.State.Region.RegionID));

            CreateMap<State, StateDto>().ForMember(dest => dest.CountryID, o => o.MapFrom(src => src.Region.CountryID));
            CreateMap<Region, RegionDto>();
            // Resource to Domain
            // CreateMap<MemberDto, Member>();
            // CreateMap<ArtistResource, Artist>();
        }
    }
}
