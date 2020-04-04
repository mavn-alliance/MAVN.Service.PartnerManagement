using System;
using System.Collections.Generic;
using Falcon.Numerics;

namespace MAVN.Service.PartnerManagement.Domain.Models
{
    /// <summary>
    /// Represents a partner.
    /// </summary>
    public class Partner
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The partner name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The an additional information about partner.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The amount in tokens to calculate rate.
        /// </summary>
        public Money18? AmountInTokens { get; set; }

        /// <summary>
        /// The amount in currency to calculate rate.
        /// </summary>
        public decimal? AmountInCurrency { get; set; }

        /// <summary>
        /// Indicates that the global currency rate should be used to convert an amount.
        /// </summary>
        public bool UseGlobalCurrencyRate { get; set; }

        /// <summary>
        /// Specifies the business vertical.
        /// </summary>
        public Vertical BusinessVertical { get; set; }

        /// <summary>
        /// A collection of partner locations (addresses).
        /// </summary>
        public IReadOnlyCollection<Location> Locations { get; set; }

        /// <summary>
        /// The partner client identifier.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The partner client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// The unique identifier of a creator.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// The date and time of creation.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
