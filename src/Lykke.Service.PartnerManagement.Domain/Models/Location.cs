using System;

namespace Lykke.Service.PartnerManagement.Domain.Models
{
    public class Location
    {
        public Guid Id { get; set; }

        public Guid PartnerId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ContactPerson ContactPerson { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ExternalId { get; set; }

        public string AccountingIntegrationCode { get; set; }
    }
}
