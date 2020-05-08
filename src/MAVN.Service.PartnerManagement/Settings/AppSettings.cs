using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using MAVN.Service.Credentials.Client;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.Sessions.Client;

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
