using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.PartnerManagement.Domain.Models;

namespace MAVN.Service.PartnerManagement.Domain.Services
{
    public interface ILocationService
    {
        Task<Location> GetByIdAsync(Guid id);

        Task<Location> GetByExternalIdAsync(string externalId);

        Task<IReadOnlyCollection<Location>> CreateLocationsContactPersonForPartnerAsync(Partner partner);

        Task<IReadOnlyCollection<Location>> UpdateRangeAsync(Partner partner,
            IReadOnlyCollection<Location> locations, IReadOnlyCollection<Location> existingLocations);
    }
}
