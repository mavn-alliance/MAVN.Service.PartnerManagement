using MAVN.Service.PartnerManagement.Client.Enums;

namespace MAVN.Service.PartnerManagement.Client.Models.Partner
{
    /// <summary>
    /// Response model for partner ability check
    /// </summary>
    public class CheckAbilityResponse
    {
        /// <summary>Boolean if the partner has ability</summary>
        public bool HasAbility { get; set; }

        /// <summary>Error code for no ability cases</summary>
        public PartnerInabilityErrorCodes ErrorCode { get; set; }
    }
}
