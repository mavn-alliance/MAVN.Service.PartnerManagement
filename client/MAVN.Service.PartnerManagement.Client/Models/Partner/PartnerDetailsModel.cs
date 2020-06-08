using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MAVN.Service.PartnerManagement.Client.Models.Location;

namespace MAVN.Service.PartnerManagement.Client.Models.Partner
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a detail model of the partner
    /// </summary>
    [PublicAPI]
    public class PartnerDetailsModel: PartnerBaseModel
    {
        /// <summary>
        /// Represents the id of the partner
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Represents a collection of the partner locations
        /// </summary>
        public IReadOnlyCollection<LocationDetailsModel> Locations { get; set; }

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
        /// Represents code which is used to refer customers
        /// </summary>
        public string ReferralCode { get; set; }
    }
}
