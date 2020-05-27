using System;

namespace MAVN.Service.PartnerManagement.Domain.Models
{
    public interface ILinkedPartner
    {
        Guid CustomerId { get; set; }
        Guid PartnerId { get; set; }
    }
}
