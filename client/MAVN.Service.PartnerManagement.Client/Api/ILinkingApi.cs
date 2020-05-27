using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.PartnerManagement.Client.Models.PartnerLinking;
using Refit;

namespace MAVN.Service.PartnerManagement.Client.Api
{
    /// <summary>
    /// Linking client API interface.
    /// </summary>
    [PublicAPI]
    public interface ILinkingApi
    {
        /// <summary>
        /// Gets partner linking info
        /// </summary>
        /// <param name="partnerId">partner id.</param>
        /// <response code="200">Partner lining info.</response>
        [Get("/api/linking/info")]
        Task<PartnerLinkingInfoResponse> GetPartnerLinkingInfoAsync([Query] Guid partnerId);

        /// <summary>
        /// Regenerates partner linking info
        /// </summary>
        /// <param name="partnerId">partner id.</param>
        [Post("/api/linking/info")]
        Task RegeneratePartnerLinkingInfoAsync([Body] Guid partnerId);

        /// <summary>
        /// Links a partner to a customer
        /// </summary>
        /// <param name="request">Request model</param>
        [Post("/api/linking")]
        Task<LinkPartnerResponse> LinkPartnerAsync([Body] LinkPartnerRequest request);

        /// <summary>
        /// Returns linked partner if exists
        /// </summary>
        /// <param name="customerId">Id of the customer</param>
        [Get("/api/linking")]
        Task<Guid?> GetLinkedPartnerAsync([Query] Guid customerId);
    }
}
