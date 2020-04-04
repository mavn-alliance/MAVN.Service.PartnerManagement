using System.Threading.Tasks;
using MAVN.Service.PartnerManagement.Domain.Models;

namespace MAVN.Service.PartnerManagement.Domain.Services
{
    public interface IAuthService
    {
        Task<AuthResult> AuthAsync(string clientId, string clientSecret, string userInfo);

        Task<string> GenerateClientId();

        Task<string> GenerateClientSecret();
    }
}
