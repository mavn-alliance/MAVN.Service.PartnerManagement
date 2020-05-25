using System.Threading.Tasks;

namespace MAVN.Service.PartnerManagement.Domain.Services
{
    public interface IGeocodingReader
    {
        Task<string> GetCountryIso3CodeByCoordinateAsync(double latitude, double longitude);
    }
}
