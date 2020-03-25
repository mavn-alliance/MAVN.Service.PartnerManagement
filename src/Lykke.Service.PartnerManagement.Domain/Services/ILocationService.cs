using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.PartnerManagement.Domain.Models;

namespace Lykke.Service.PartnerManagement.Domain.Services
{
    public interface ILocationService
    {
        Task<Location> GetByIdAsync(string id);

        Task<Location> GetByExternalIdAsync(string externalId);

        Task<IReadOnlyCollection<Location>> CreateLocationsContactPersonForPartnerAsync(Partner partner);

        Task<IReadOnlyCollection<Location>> UpdateRangeAsync(Partner partner,
            IReadOnlyCollection<Location> locations, IReadOnlyCollection<Location> existingLocations);
    }
}
