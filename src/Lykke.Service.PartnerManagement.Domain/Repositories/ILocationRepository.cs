using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.PartnerManagement.Domain.Models;

namespace Lykke.Service.PartnerManagement.Domain.Repositories
{
    public interface ILocationRepository
    {
        Task<bool> DoesExistAsync(string externalId, Guid? id);
        Task<Location> GetByIdAsync(string id);
        Task<Location> GetByExternalIdAsync(string externalId);
        Task<bool> AreExternalIdsNotUniqueAsync(Guid partnerId, IEnumerable<string> externalIds);
    }
}
