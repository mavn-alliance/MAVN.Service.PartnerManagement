using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Models.Dto;

namespace MAVN.Service.PartnerManagement.Domain.Services
{
    public interface IPartnerService
    {
        Task<Guid> CreateAsync(Partner partner);
        Task UpdateAsync(Partner partner);
        Task DeleteAsync(Guid partnerId);
        Task<(IReadOnlyCollection<Partner> partners, int totalSize)> GetAsync(PartnerListRequestDto model);
        Task<Partner> GetByIdAsync(Guid partnerId);
        Task<Partner> GetByClientIdAsync(string clientId);
        Task<Partner> GetByLocationIdAsync(Guid locationId);
        Task<IReadOnlyCollection<Partner>> GetByIdsAsync(IEnumerable<Guid> ids);
    }
}
