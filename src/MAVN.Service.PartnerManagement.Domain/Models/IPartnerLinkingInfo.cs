using System;

namespace MAVN.Service.PartnerManagement.Domain.Models
{
    public interface IPartnerLinkingInfo
    {
        Guid PartnerId { get; set; }
        string PartnerCode { get; set; }
        string PartnerLinkingCode { get; set; }
    }
}
