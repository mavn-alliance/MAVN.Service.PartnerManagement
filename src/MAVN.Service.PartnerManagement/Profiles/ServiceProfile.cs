using AutoMapper;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using MAVN.Service.PartnerManagement.Client.Models;
using MAVN.Service.PartnerManagement.Client.Models.Authentication;
using MAVN.Service.PartnerManagement.Client.Models.Location;
using MAVN.Service.PartnerManagement.Client.Models.Partner;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Models.Dto;

namespace MAVN.Service.PartnerManagement.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<PartnerListRequestModel, PartnerListRequestDto>();
            CreateMap<Client.Models.Vertical, Domain.Models.Vertical>();

            CreateMap<PartnerCreateModel, Partner>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<PartnerUpdateModel, Partner>(MemberList.Destination)
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<LocationCreateModel, Location>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PartnerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Geohash, opt => opt.Ignore());

            CreateMap<LocationUpdateModel, Location>(MemberList.Destination)
                .ForMember(dest => dest.PartnerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Geohash, opt => opt.Ignore());

            CreateMap<Location, LocationInfoResponse>(MemberList.Destination);

            CreateMap<ContactPersonModel, ContactPerson>(MemberList.Destination);

            CreateMap<PartnerContactResponse, ContactPerson>(MemberList.Destination)
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.PartnerContact.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.PartnerContact.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.PartnerContact.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PartnerContact.PhoneNumber));

            CreateMap<Partner, PartnerListDetailsModel>(MemberList.Destination);
            CreateMap<Location, PartnerListLocationModel>(MemberList.Destination);

            CreateMap<ContactPerson, ContactPersonModel>(MemberList.Destination);
            CreateMap<Location, LocationDetailsModel>(MemberList.Destination);
            CreateMap<Partner, PartnerDetailsModel>(MemberList.Destination);

            CreateMap<AuthResult, AuthenticateResponseModel>(MemberList.Destination);
        }
    }
}
