using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PartnerManagement.Client 
{
    /// <summary>
    /// PartnerManagement client settings.
    /// </summary>
    public class PartnerManagementServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
