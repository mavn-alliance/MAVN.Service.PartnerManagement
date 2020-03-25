using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lykke.Service.PartnerManagement.MsSqlRepositories.Entities
{
    [Table("location")]
    public class LocationEntity : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("partner_id")]
        public Guid PartnerId { get; set; }

        [Column("external_id")]
        public string ExternalId { get; set; }

        public PartnerEntity Partner { get; set; }

        [Column("accounting_integration_code")]
        [Required]
        public string AccountingIntegrationCode { get; set; }
    }
}
