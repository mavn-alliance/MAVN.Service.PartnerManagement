using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.MsSql;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MsSqlContextFactory<PartnerManagementContext> _msSqlContextFactory;
        private readonly IMapper _mapper;

        public LocationRepository(
            MsSqlContextFactory<PartnerManagementContext> msSqlContextFactory,
            IMapper mapper)
        {
            _msSqlContextFactory = msSqlContextFactory;
            _mapper = mapper;
        }

        public async Task<bool> DoesExistAsync(string externalId, Guid? id = null)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                return await context.Locations
                    .AnyAsync(l => id == null ?
                        l.ExternalId == externalId :
                        l.Id != id && l.ExternalId == externalId);
            }
        }

        public async Task<Location> GetByIdAsync(string id)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var result = await context.Locations.FindAsync(id);

                return _mapper.Map<Location>(result);
            }
        }

        public async Task<Location> GetByExternalIdAsync(string externalId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var result = await context.Locations
                    .FirstOrDefaultAsync(l => l.ExternalId == externalId);

                return _mapper.Map<Location>(result);
            }
        }

        public async Task<bool> AreExternalIdsNotUniqueAsync(Guid partnerId, IEnumerable<string> externalIds)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
               return await context.Locations.AsNoTracking()
                    .AnyAsync(l => externalIds.Contains(l.ExternalId)
                                   && l.PartnerId != partnerId);
            }
        }
    }
}
