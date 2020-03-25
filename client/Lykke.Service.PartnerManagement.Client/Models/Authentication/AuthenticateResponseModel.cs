using JetBrains.Annotations;
using Lykke.Service.PartnerManagement.Client.Enums;

namespace Lykke.Service.PartnerManagement.Client.Models.Authentication
{
    /// <summary>
    /// Authenticate response model.
    /// </summary>
    [PublicAPI]
    public class AuthenticateResponseModel
    {
        /// <summary>Token</summary>
        public string Token { get; set; }

        /// <summary>Error</summary>
        public PartnerManagementError Error { get; set; }
    }
}
