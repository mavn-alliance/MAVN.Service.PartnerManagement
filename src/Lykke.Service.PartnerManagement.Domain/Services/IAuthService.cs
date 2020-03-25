using System.Threading.Tasks;
using Lykke.Service.PartnerManagement.Domain.Models;

namespace Lykke.Service.PartnerManagement.Domain.Services
{
    public interface IAuthService
    {
        Task<AuthResult> AuthAsync(string clientId, string clientSecret, string userInfo);

        Task<string> GenerateClientId();

        Task<string> GenerateClientSecret();
    }
}
