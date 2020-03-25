using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Lykke.Service.PartnerManagement.Client.Models.Authentication
{
    /// <summary>
    /// Authenticate request model.
    /// </summary>
    [PublicAPI]
    public class AuthenticateRequestModel
    {
        /// <summary>Client Id.</summary>
        [Required]
        public string ClientId { get; set; }

        /// <summary>Client Secret.</summary>
        [Required]
        public string ClientSecret { get; set; }

        /// <summary>User Info.</summary>
        public string UserInfo { get; set; }
    }
}
