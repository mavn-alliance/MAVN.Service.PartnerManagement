using System;
using JetBrains.Annotations;

namespace MAVN.Service.PartnerManagement.Client.Models.Partner
{
    /// <summary>
    /// Location model for the partner list request
    /// </summary>
    [PublicAPI]
    public class PartnerListLocationModel
    {
        /// <summary>
        /// Id of the location
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the location
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Longitude of the location
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Latitude of the location
        /// </summary>
        public double? Latitude { get; set; }
    }
}
