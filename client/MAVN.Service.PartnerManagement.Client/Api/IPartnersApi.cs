using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.PartnerManagement.Client.Models.Partner;
using Refit;

namespace MAVN.Service.PartnerManagement.Client.Api
{
    /// <summary>
    /// PartnerManagement client API interface.
    /// </summary>
    [PublicAPI]
    public interface IPartnersApi
    {
        /// <summary>
        /// Gets all partners paginated.
        /// </summary>
        /// <param name="request">The paginated list request parameters.</param>
        /// <returns>A collection of partners.</returns>
        [Get("/api/partners")]
        Task<PartnerListResponseModel> GetAsync(PartnerListRequestModel request);

        /// <summary>
        /// Gets partner by identifier.
        /// </summary>
        /// <param name="id">The partner identifier.</param>
        /// <returns>A detailed partner information.</returns>
        [Get("/api/partners/{id}")]
        Task<PartnerDetailsModel> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets partner by client identifier.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>A detailed partner information.</returns>
        [Get("/api/partners/byClientId/{clientId}")]
        Task<PartnerDetailsModel> GetByClientIdAsync(string clientId);

        /// <summary>
        /// Gets partner by location identifier.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns>A detailed partner information.</returns>
        [Get("/api/partners/byLocationId/{locationId}")]
        Task<PartnerDetailsModel> GetByLocationIdAsync(Guid locationId);

        /// <summary>
        /// Creates a partner.
        /// </summary>
        /// <param name="partnerCreateModel">The partner creation details.</param>
        /// <returns>The result of partner creation.</returns>
        [Post("/api/partners")]
        Task<PartnerCreateResponse> CreateAsync(PartnerCreateModel partnerCreateModel);

        /// <summary>
        /// Updates partner.
        /// </summary>
        /// <param name="partnerUpdateModel">The partner update details.</param>
        /// <returns>The result of partner update.</returns>
        [Put("/api/partners")]
        Task<PartnerUpdateResponse> UpdateAsync(PartnerUpdateModel partnerUpdateModel);

        /// <summary>
        /// Deletes partner by identifier.
        /// </summary>
        /// <param name="id">The partner identifier.</param>
        [Delete("/api/partners/{id}")]
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Gets partners by collect of identifier.
        /// </summary>
        /// <param name="ids">The partners identifiers.</param>
        /// <response code="200">A collection of partners.</response>
        [Post("/api/partners/list")]
        Task<IReadOnlyCollection<PartnerListDetailsModel>> GetByIdsAsync(Guid[] ids);

        /// <summary>
        /// Check if partner has ability to do something
        /// </summary>
        /// <param name="request">.</param>
        /// <response code="200">Check ability response.</response>
        [Get("/api/partners/ability/check")]
        Task<CheckAbilityResponse> CheckAbilityAsync([Query] CheckAbilityRequest request);

        /// <summary>
        /// Gets ids of partners which are close to passed longitude and latitude
        /// </summary>
        /// <param name="request">request parameters.</param>
        /// <response code="200">A collection of partner ids.</response>
        [Get("/api/partners/byCoordinates")]
        Task<GetNearPartnersByCoordinatesResponse> GetNearPartnerByCoordinatesAsync(
            [Query] GetNearPartnersByCoordinatesRequest request);
    }
}
