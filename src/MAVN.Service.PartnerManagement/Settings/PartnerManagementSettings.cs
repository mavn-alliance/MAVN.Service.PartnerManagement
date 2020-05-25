using JetBrains.Annotations;

namespace MAVN.Service.PartnerManagement.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PartnerManagementSettings
    {
        public DbSettings Db { get; set; }

        public AuthenticationSettings Authentication { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }

        public GeocodingSettings Geocoding { get; set; }
    }
}
