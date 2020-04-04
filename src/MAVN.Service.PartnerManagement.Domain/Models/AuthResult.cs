using MAVN.Service.PartnerManagement.Domain.Services;

namespace MAVN.Service.PartnerManagement.Domain.Models
{
    public class AuthResult
    {
        public string Token { get; set; }

        public ServicesError Error { get; set; }
    }
}
