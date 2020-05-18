using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.PartnerManagement.Domain.Models;

namespace MAVN.Service.PartnerManagement.Domain.Repositories
{
    public interface ILocationRepository
    {
        Task<bool> DoesExistAsync(string externalId, Guid? id);
        Task<Location> GetByIdAsync(Guid id);
        Task<Location> GetByExternalIdAsync(string externalId);
        Task<bool> AreExternalIdsNotUniqueAsync(Guid partnerId, IEnumerable<string> externalIds);
        Task<IReadOnlyCollection<Location>> GetLocationsByGeohashAsync(string geohash);
    }
}
