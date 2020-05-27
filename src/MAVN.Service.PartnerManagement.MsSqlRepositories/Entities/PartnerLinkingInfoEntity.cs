using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.PartnerManagement.Domain.Models;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Entities
{
    [Table("partner_linking_info")]
    public class PartnerLinkingInfoEntity : IPartnerLinkingInfo
    {
        [Key]
        [Column("partner_id")]
        public Guid PartnerId { get; set; }

        [Required]
        [Column("partner_code")]
        public string PartnerCode { get; set; }

        [Required]
        [Column("partner_linking_code")]
        public string PartnerLinkingCode { get; set; }

        public static PartnerLinkingInfoEntity Create(IPartnerLinkingInfo model)
        {
            return new PartnerLinkingInfoEntity
            {
                PartnerCode = model.PartnerCode,
                PartnerLinkingCode = model.PartnerLinkingCode,
                PartnerId = model.PartnerId,
            };
        }
    }
}
