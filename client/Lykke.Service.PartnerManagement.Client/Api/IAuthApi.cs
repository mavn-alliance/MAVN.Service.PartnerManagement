using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.PartnerManagement.Client.Models.Authentication;
using Refit;

namespace Lykke.Service.PartnerManagement.Client.Api
{
    /// <summary>
    /// Auth client API interface.
    /// </summary>
    [PublicAPI]
    public interface IAuthApi
    {
        /// <summary>
        /// Authenticates customer in the system.
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns><see cref="AuthenticateResponseModel"/></returns>
        [Post("/api/auth/login")]
        Task<AuthenticateResponseModel> AuthenticateAsync(AuthenticateRequestModel request);

        /// <summary>
        /// Generate partner client id.
        /// </summary>
        /// <returns><see cref="string"/></returns>
        [Post("/api/auth/generateClientSecret")]
        Task<string> GenerateClientSecret();

        /// <summary>
        /// Generate partner client secret.
        /// </summary>
        /// <returns><see cref="string"/></returns>
        [Post("/api/auth/generateClientId")]
        Task<string> GenerateClientId();
    }
}
