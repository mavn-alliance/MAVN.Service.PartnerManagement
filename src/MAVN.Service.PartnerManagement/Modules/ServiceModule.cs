using Autofac;
using JetBrains.Annotations;
using Lykke.Service.Credentials.Client;
using MAVN.Service.PartnerManagement.Domain.Services;
using MAVN.Service.PartnerManagement.DomainServices;
using MAVN.Service.PartnerManagement.MsSqlRepositories;
using MAVN.Service.PartnerManagement.Settings;
using Lykke.Service.Sessions.Client;
using Lykke.SettingsReader;

namespace MAVN.Service.PartnerManagement.Modules
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(
                new AutofacModule(_appSettings.CurrentValue.PartnerManagementService.Db.MsSqlConnectionString));

            builder.RegisterType<PartnerService>()
                .As<IPartnerService>();

            builder.RegisterType<LocationService>()
                .As<ILocationService>();

            builder.RegisterType<AuthService>()
                .WithParameter("sessionsServiceTokenTimeToLiveInSeconds",
                    _appSettings.CurrentValue.PartnerManagementService.Authentication
                        .SessionsServiceTokenTimeToLiveInSeconds)
                .WithParameter("usernameLength",
                    _appSettings.CurrentValue.PartnerManagementService.Authentication.GeneratedUsernameLength)
                .WithParameter("passwordLength",
                    _appSettings.CurrentValue.PartnerManagementService.Authentication.GeneratedPasswordLength)
                .As<IAuthService>();

            builder.RegisterCredentialsClient(_appSettings.CurrentValue.CredentialsServiceClient);

            builder.RegisterSessionsServiceClient(_appSettings.CurrentValue.SessionsServiceClient);
        }
    }
}
