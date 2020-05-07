using System;
using System.Collections.Generic;
using MAVN.Service.PartnerManagement.Client.Models.Location;

namespace MAVN.Service.PartnerManagement.Client.Models.Partner
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a detail model of the partner
    /// </summary>
    public class PartnerListDetailsModel : PartnerBaseModel
    {
        /// <summary>
        /// Represents the id of the partner
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Represents the client id(username) of the partner
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Represents the creator of the partner
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Represents the date of creation of the partner
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Partner locations details
        /// </summary>
        public IReadOnlyCollection<PartnerListLocationModel> Locations { get; set; }
    }
}
