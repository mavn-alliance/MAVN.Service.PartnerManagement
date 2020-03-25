using System;

namespace Lykke.Service.PartnerManagement.Client.Models.Location
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a location details model
    /// </summary>
    public class LocationDetailsModel: LocationBaseModel
    {
        /// <summary>
        /// The location id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The creator of the location
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// The timestamp of the creation
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
