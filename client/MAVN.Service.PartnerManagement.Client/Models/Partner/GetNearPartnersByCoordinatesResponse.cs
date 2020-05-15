using System;

namespace MAVN.Service.PartnerManagement.Client.Models.Partner
{
    /// <summary>
    /// response model
    /// </summary>
    public class GetNearPartnersByCoordinatesResponse
    {
        /// <summary>
        /// Ids of the partners
        /// </summary>
        public Guid[] PartnersIds { get; set; }
    }
}
