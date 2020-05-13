using Autofac;
using JetBrains.Annotations;
using MAVN.Service.Credentials.Client;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.PartnerManagement.Settings;
using MAVN.Service.Sessions.Client;
using Lykke.SettingsReader;
using MAVN.Service.PaymentManagement.Client;

namespace MAVN.Service.PartnerManagement.Modules
{
    [UsedImplicitly]
    public class ClientsModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ClientsModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterCredentialsClient(_appSettings.CurrentValue.CredentialsServiceClient);

            builder.RegisterSessionsServiceClient(_appSettings.CurrentValue.SessionsServiceClient);

            builder.RegisterCustomerProfileClient(_appSettings.CurrentValue.CustomerProfileServiceClient);

            builder.RegisterPaymentManagementClient(_appSettings.CurrentValue.PaymentManagementServiceClient, null);
        }
    }
}
