using Autofac;
using JetBrains.Annotations;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.SettingsReader;
using MAVN.Service.PartnerManagement.Contract;
using MAVN.Service.PartnerManagement.Settings;

namespace MAVN.Service.PartnerManagement.Modules
{
    [UsedImplicitly]
    public class RabbitMqModule : Module
    {
        private const string PartnerCreatedExchangeName = "lykke.customer.partnercreated";

        private readonly string _connString;

        public RabbitMqModule(IReloadingManager<AppSettings> appSettings)
        {
            _connString = appSettings.CurrentValue.PartnerManagementService.RabbitMq.RabbitMqConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterJsonRabbitPublisher<PartnerCreatedEvent>(
                _connString,
                PartnerCreatedExchangeName);
        }
    }
}
