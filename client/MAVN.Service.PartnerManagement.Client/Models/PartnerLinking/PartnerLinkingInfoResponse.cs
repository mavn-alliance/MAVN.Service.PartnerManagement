using System;

namespace MAVN.Service.PartnerManagement.Client.Models.PartnerLinking
{
    /// <summary>
    /// Holds info about partner linking
    /// </summary>
    public class PartnerLinkingInfoResponse
    {
        /// <summary>
        /// Id of the partner
        /// </summary>
        public Guid PartnerId { get; set; }
        /// <summary>
        /// Code of the partner
        /// </summary>
        public string PartnerCode { get; set; }
        /// <summary>
        /// Code used for linking of the partner
        /// </summary>
        public string PartnerLinkingCode { get; set; }
    }
}
