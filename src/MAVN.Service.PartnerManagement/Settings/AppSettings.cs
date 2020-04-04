using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using Lykke.Service.Credentials.Client;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.Sessions.Client;

namespace MAVN.Service.PartnerManagement.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public PartnerManagementSettings PartnerManagementService { get; set; }

        public CredentialsServiceClientSettings CredentialsServiceClient { get; set; }

        public SessionsServiceClientSettings SessionsServiceClient { get; set; }

        public CustomerProfileServiceClientSettings CustomerProfileServiceClient { get; set; }
    }
}
