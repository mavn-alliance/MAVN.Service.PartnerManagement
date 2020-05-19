using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.PartnerManagement.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PartnerManagementSettings
    {
        public DbSettings Db { get; set; }

        public AuthenticationSettings Authentication { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }
    }
}
