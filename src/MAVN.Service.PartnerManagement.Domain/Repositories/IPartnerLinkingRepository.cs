using System;
using System.Threading.Tasks;
using MAVN.Service.PartnerManagement.Domain.Models;

namespace MAVN.Service.PartnerManagement.Domain.Repositories
{
    public interface IPartnerLinkingRepository
    {
        Task<IPartnerLinkingInfo> GetPartnerLinkingInfoAsync(Guid partnerId);
        Task<IPartnerLinkingInfo> GetPartnerLinkingInfoByPartnerCodeAsync(string partnerCode);
        Task AddPartnerLinkingInfoAsync(IPartnerLinkingInfo model);
        Task<bool> PartnerCodeExistsAsync(string partnerCode);
        Task<bool> CustomerHasLinkAsync(Guid customerId);
        Task AddPartnerLinkAsync(ILinkedPartner model);
        Task<Guid?> GetLinkedPartnerByCustomerIdAsync(Guid customerId);
        Task AddOrUpdatePartnerLinkingInfoAsync(IPartnerLinkingInfo model);
    }
}
