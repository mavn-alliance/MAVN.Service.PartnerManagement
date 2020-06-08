using AutoMapper;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.MsSqlRepositories.Entities;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PartnerEntity, Partner>(MemberList.Destination)
                .ForMember(dest => dest.ClientSecret, opt => opt.Ignore())
                .ForMember(dest => dest.ReferralCode, opt => opt.Ignore());

            CreateMap<Partner, PartnerEntity>(MemberList.Destination)
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => src.Locations));

            CreateMap<Location, LocationEntity>(MemberList.Destination)
                .ForMember(dest => dest.PartnerId, opt => opt.Ignore())
                .ForMember(dest => dest.
                    Partner, opt => opt.Ignore());

            CreateMap<LocationEntity, Location>(MemberList.Destination)
                .ForMember(dest => dest.ContactPerson, opt => opt.Ignore());
        }
    }
}
