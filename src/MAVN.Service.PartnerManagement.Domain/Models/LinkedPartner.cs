using System;

namespace MAVN.Service.PartnerManagement.Domain.Models
{
    public class LinkedPartner : ILinkedPartner
    {
        public Guid CustomerId { get; set; }
        public Guid PartnerId { get; set; }
    }
}
