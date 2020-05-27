using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.PartnerManagement.Domain.Models;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Entities
{
    [Table("linked_partners")]
    public class LinkedPartnerEntity : ILinkedPartner
    {
        [Key]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Required]
        [Column("partner_id")]
        public Guid PartnerId { get; set; }

        public static LinkedPartnerEntity Create(ILinkedPartner model)
        {
            return new LinkedPartnerEntity
            {
                PartnerId = model.PartnerId,
                CustomerId = model.CustomerId,
            };
        }
    }
}
