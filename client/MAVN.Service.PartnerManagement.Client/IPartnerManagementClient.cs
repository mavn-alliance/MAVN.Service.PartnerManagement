using JetBrains.Annotations;
using MAVN.Service.PartnerManagement.Client.Api;

namespace MAVN.Service.PartnerManagement.Client
{
    /// <summary>
    /// PartnerManagement client interface.
    /// </summary>
    [PublicAPI]
    public interface IPartnerManagementClient
    {
        /// <summary>
        /// Authentication API.
        /// </summary>
        IAuthApi Auth { get; }

        /// <summary>
        /// Locations API.
        /// </summary>
        ILocationsApi Locations { get; }

        /// <summary>
        /// Partners API.
        /// </summary>
        IPartnersApi Partners { get; }

        /// <summary>
        /// Linking API.
        /// </summary>
        ILinkingApi Linking { get; }
    }
}
