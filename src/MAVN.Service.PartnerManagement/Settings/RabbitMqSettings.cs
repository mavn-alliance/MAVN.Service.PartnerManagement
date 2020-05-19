using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.PartnerManagement.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string RabbitMqConnectionString { get; set; }
    }
}
