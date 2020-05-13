using System;
using System.ComponentModel.DataAnnotations;

namespace MAVN.Service.PartnerManagement.Client.Models.Partner
{
    /// <summary>
    /// Request model for partner ability check
    /// </summary>
    public class CheckAbilityRequest
    {
        /// <summary>
        /// The ability which you want to check
        /// </summary>
        [Required]
        public PartnerAbility? PartnerAbility { get; set; }

        /// <summary>
        /// Id of the partner
        /// </summary>
        [Required]
        public Guid PartnerId { get; set; }
    }
}
