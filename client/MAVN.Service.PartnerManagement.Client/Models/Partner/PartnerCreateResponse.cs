using System;

namespace MAVN.Service.PartnerManagement.Client.Models.Partner
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a response model for partner creation
    /// </summary>
    public class PartnerCreateResponse: PartnerManagementErrorResponseModel
    {
        /// <summary>
        /// The id of the created partner
        /// </summary>
        public Guid Id { get; set; }
    }
}
