using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PartnerManagement.Settings
{
    public class AuthenticationSettings
    {
        public int SessionsServiceTokenTimeToLiveInSeconds { get; set; }

        public int GeneratedUsernameLength { get; set; }

        public int GeneratedPasswordLength { get; set; }
    }
}
