using Lykke.HttpClientGenerator;
using MAVN.Service.PartnerManagement.Client.Api;

namespace MAVN.Service.PartnerManagement.Client
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
            Linking = httpClientGenerator.Generate<ILinkingApi>();
        }

        /// <inheritdoc/>
        public IAuthApi Auth { get; }

        /// <inheritdoc/>
        public ILocationsApi Locations { get; }

        /// <inheritdoc/>
        public IPartnersApi Partners { get; }

        /// <inheritdoc/>
        public ILinkingApi Linking { get; }
    }
}
