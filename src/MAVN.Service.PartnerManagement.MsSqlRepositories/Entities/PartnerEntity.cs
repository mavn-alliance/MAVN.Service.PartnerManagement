using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Numerics;
using MAVN.Service.PartnerManagement.Domain.Models;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Entities
{
    [Table("partner")]
    public class PartnerEntity: BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("business_vertical")]
        public Vertical BusinessVertical { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("client_id")]
        public string ClientId { get; set; }

        [Column("amount_in_tokens", TypeName = "nvarchar(64)")]
        public Money18? AmountInTokens { get; set; }

        [Column("amount_in_currency")]
        public decimal? AmountInCurrency { get; set; }
        
        [Column("use_global_currency_rate")]
        public bool UseGlobalCurrencyRate { get; set; }

        public ICollection<LocationEntity> Locations { get; set; }
    }
}
