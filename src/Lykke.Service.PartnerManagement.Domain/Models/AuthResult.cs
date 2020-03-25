using Lykke.Service.PartnerManagement.Domain.Services;

namespace Lykke.Service.PartnerManagement.Domain.Models
{
    public class AuthResult
    {
        public string Token { get; set; }

        public ServicesError Error { get; set; }
    }
}
