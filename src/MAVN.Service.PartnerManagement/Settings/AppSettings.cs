using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using MAVN.Service.Credentials.Client;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.PaymentManagement.Client;
using MAVN.Service.Referral.Client;
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

        public PaymentManagementServiceClientSettings PaymentManagementServiceClient { get; set; }

        public ReferralServiceClientSettings ReferralServiceClient { get; set; }
    }
}
