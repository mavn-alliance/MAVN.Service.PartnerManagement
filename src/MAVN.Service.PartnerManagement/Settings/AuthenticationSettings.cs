using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.PartnerManagement.Settings
{
    public class AuthenticationSettings
    {
        public int SessionsServiceTokenTimeToLiveInSeconds { get; set; }

        public int GeneratedUsernameLength { get; set; }

        public int GeneratedPasswordLength { get; set; }
    }
}
