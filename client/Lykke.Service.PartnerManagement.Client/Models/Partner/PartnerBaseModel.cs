using Falcon.Numerics;

namespace Lykke.Service.PartnerManagement.Client.Models.Partner
{
    /// <summary>
    /// Represents base structure of the partner model
    /// </summary>
    public abstract class PartnerBaseModel
    {
        /// <summary>
        /// Represents the name of the partner
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents the description of the partner
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
        /// Represents the Business Vertical
        /// </summary>
        public Vertical? BusinessVertical { get; set; }
    }
}
