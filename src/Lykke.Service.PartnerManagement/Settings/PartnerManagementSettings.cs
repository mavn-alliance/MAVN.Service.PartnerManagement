using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PartnerManagement.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PartnerManagementSettings
    {
        public DbSettings Db { get; set; }

        public AuthenticationSettings Authentication { get; set; }
    }
}
