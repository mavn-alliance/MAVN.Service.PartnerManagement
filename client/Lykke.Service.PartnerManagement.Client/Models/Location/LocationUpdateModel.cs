using System;

namespace Lykke.Service.PartnerManagement.Client.Models.Location
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a location model
    /// </summary>
    public class LocationUpdateModel: LocationBaseModel
    {
        /// <summary>
        /// The location id
        /// </summary>
        public Guid Id { get; set; }
    }
}
