using Autofac;
using JetBrains.Annotations;
using Lykke.Service.Credentials.Client;
using Lykke.Service.CustomerProfile.Client;
using MAVN.Service.PartnerManagement.Settings;
using Lykke.Service.Sessions.Client;
using Lykke.SettingsReader;

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
        }
    }
}
