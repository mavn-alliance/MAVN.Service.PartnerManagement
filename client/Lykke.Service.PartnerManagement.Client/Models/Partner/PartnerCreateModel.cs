using System;
using System.Collections.Generic;
using Lykke.Service.PartnerManagement.Client.Models.Location;

namespace Lykke.Service.PartnerManagement.Client.Models.Partner
{
    /// <inheritdoc />
    /// <summary>
    /// Represents partner create model
    /// </summary>
    public class PartnerCreateModel: PartnerBaseModel
    {
        /// <summary>
        /// Represents a collection of the partner locations
        /// </summary>
        public IReadOnlyCollection<LocationCreateModel> Locations { get; set; }

        /// <summary>
        /// The client customer id - Empty for no change
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The client customer secret - Empty for no change
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Represents the creator of the partner
        /// </summary>
        public Guid CreatedBy { get; set; }
    }
}
