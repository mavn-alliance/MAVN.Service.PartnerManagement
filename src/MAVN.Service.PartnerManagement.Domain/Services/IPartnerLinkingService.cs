using System;
using System.Threading.Tasks;
using MAVN.Service.PartnerManagement.Domain.Enums;
using MAVN.Service.PartnerManagement.Domain.Models;

namespace MAVN.Service.PartnerManagement.Domain.Services
{
    public interface IPartnerLinkingService
    {
        Task<IPartnerLinkingInfo> GetOrAddPartnerLinkingInfoAsync(Guid partnerId);
        Task<PartnerLinkingErrorCodes> LinkPartnerAndCustomerAsync(Guid customerId, string partnerCode, string partnerLinkingCode);
        Task<Guid?> GetLinkedPartnerAsync(Guid customerId);
        Task AddOrUpdatePartnerLinkingInfoAsync(Guid partnerId);
    }
}
