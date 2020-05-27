using MAVN.Service.PartnerManagement.Client.Enums;

namespace MAVN.Service.PartnerManagement.Client.Models.PartnerLinking
{
    /// <summary>
    /// Response for partner link operation
    /// </summary>
    public class LinkPartnerResponse
    {
        /// <summary>
        /// Error code
        /// </summary>
        public PartnerLinkingErrorCode Error { get; set; }
    }
}
