using System;

namespace MAVN.Service.PartnerManagement.Domain.Models
{
    public class PartnerLinkingInfo : IPartnerLinkingInfo
    {
        public Guid PartnerId { get; set; }
        public string PartnerCode { get; set; }
        public string PartnerLinkingCode { get; set; }
    }
}
