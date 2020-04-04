using System.Collections.Generic;

namespace MAVN.Service.PartnerManagement.Client.Models.Partner
{
    /// <inheritdoc />
    /// <summary>
    /// Represents list response model
    /// </summary>
    public class PartnerListResponseModel: PartnerManagementErrorResponseModel
    {
        /// <summary>
        /// A collection of partner details
        /// </summary>
        public IReadOnlyCollection<PartnerListDetailsModel> PartnersDetails { get; set; }

        /// <summary>
        /// The current page the response is at
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The total size of all partners
        /// </summary>
        public int TotalSize { get; set; }
    }
}
