using System.Threading.Tasks;

namespace MAVN.Service.PartnerManagement.Domain.Services
{
    public interface IGeocodingRestService
    {
        Task<string> GetCountryDataByCoordinateAsync(double latitude, double longitude);
    }
}
