using Lykke.HttpClientGenerator;
using Lykke.Service.PartnerManagement.Client.Api;

namespace Lykke.Service.PartnerManagement.Client
{
    /// <inheritdoc/>
    public class PartnerManagementClient : IPartnerManagementClient
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PartnerManagementClient"/> with <param name="httpClientGenerator"></param>.
        /// </summary> 
        public PartnerManagementClient(IHttpClientGenerator httpClientGenerator)
        {
            Auth = httpClientGenerator.Generate<IAuthApi>();
            Locations = httpClientGenerator.Generate<ILocationsApi>();
            Partners = httpClientGenerator.Generate<IPartnersApi>();
        }

        /// <inheritdoc/>
        public IAuthApi Auth { get; }

        /// <inheritdoc/>
        public ILocationsApi Locations { get; }

        /// <inheritdoc/>
        public IPartnersApi Partners { get; }
    }
}
