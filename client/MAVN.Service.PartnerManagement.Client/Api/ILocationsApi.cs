using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.PartnerManagement.Client.Models.Location;
using Refit;

namespace MAVN.Service.PartnerManagement.Client.Api
{
    /// <summary>
    /// PartnerManagement client API interface.
    /// </summary>
    [PublicAPI]
    public interface ILocationsApi
    {
        /// <summary>
        /// Gets location by external identifier.
        /// </summary>
        /// <param name="externalId">string</param>
        /// <returns><see cref="LocationInfoResponse"/></returns>
        [Get("/api/locations/byexternalid")]
        Task<LocationInfoResponse> GetByExternalId2Async(string externalId);

        /// <summary>
        /// Gets location by id.
        /// </summary>
        /// <param name="id">string</param>
        /// <returns><see cref="LocationInfoResponse"/></returns>
        [Get("/api/locations/byid")]
        Task<LocationInfoResponse> GetByIdAsync(string id);
    }
}
