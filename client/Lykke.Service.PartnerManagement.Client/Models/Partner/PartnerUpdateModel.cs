using System;
using System.Collections.Generic;
using Lykke.Service.PartnerManagement.Client.Models.Location;

namespace Lykke.Service.PartnerManagement.Client.Models.Partner
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a model use for partner update
    /// </summary>
    public class PartnerUpdateModel: PartnerBaseModel
    {
        /// <summary>
        /// The Id of the partner
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The client customer id - Empty for no change
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The client customer secret - Empty for no change
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Represents a collection of the partner locations
        /// </summary>
        public IReadOnlyCollection<LocationUpdateModel> Locations { get; set; }
    }
}
