using System;

namespace MAVN.Service.PartnerManagement.Client.Models.PartnerLinking
{
    /// <summary>
    /// Request model used to link partner to a customer
    /// </summary>
    public class LinkPartnerRequest
    {
        /// <summary>
        /// Id of the customer
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Code of the partner
        /// </summary>
        public string PartnerCode { get; set; }

        /// <summary>
        /// Linking code of the partner
        /// </summary>
        public string PartnerLinkingCode { get; set; }
    }
}
