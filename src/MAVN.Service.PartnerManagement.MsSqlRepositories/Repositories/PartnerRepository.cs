using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.MsSql;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Repositories;
using MAVN.Service.PartnerManagement.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using MoreLinq;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Repositories
{
    public class PartnerRepository: IPartnerRepository
    {
        private readonly MsSqlContextFactory<PartnerManagementContext> _msSqlContextFactory;
        private readonly IMapper _mapper;

        public PartnerRepository(
            MsSqlContextFactory<PartnerManagementContext> msSqlContextFactory,
            IMapper mapper)
        {
            _msSqlContextFactory = msSqlContextFactory;
            _mapper = mapper;
        }

        public async Task<Partner> CreateAsync(Partner partner)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var entity = _mapper.Map<PartnerEntity>(partner);

                var createdAt = DateTime.UtcNow;

                entity.CreatedAt = createdAt;
                entity.Locations.ForEach(l =>
                {
                    l.CreatedAt = createdAt;
                });

                await context.AddAsync(entity);

                await context.SaveChangesAsync();
                
                return _mapper.Map<Partner>(entity);
            }
        }

        public async Task UpdateAsync(Partner partner)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var entity = _mapper.Map<PartnerEntity>(partner);

                entity.Locations.ForEach(l => l.PartnerId = entity.Id);

                using (var transaction = context.Database.BeginTransaction())
                {
                    foreach (var entityLocation in entity.Locations)
                    {
                        if (context.Locations.Any(l => l.Id == entityLocation.Id))
                        {
                            context.Update(entityLocation);
                        }
                        else
                        {
                            context.Add(entityLocation);
                        }
                    }

                    // EF cannot evaluate such complex query so we need to do one part of the filtration here
                    // And the second part after ToList
                    var locationsToDelete = context.Locations.Where(l =>
                        l.PartnerId == entity.Id).ToList();

                    locationsToDelete = locationsToDelete
                        .Where(l => entity.Locations.All(e => e.Id != l.Id)).ToList();

                    if (locationsToDelete.Any())
                    {
                        context.RemoveRange(locationsToDelete);
                    }

                    // Skip updating the locations relationship because we did it above
                    entity.Locations = null;

                    context.Update(entity);

                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
            }
        }

        public async Task DeleteAsync(Guid partnerId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var entity = await context.Partners.FirstOrDefaultAsync(p => p.Id == partnerId);

                if (entity != null)
                {
                    context.Remove(entity);

                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<(IReadOnlyCollection<Partner> partners, int totalSize)> GetAsync(int page, int pageSize, string name, Vertical? vertical)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                IQueryable<PartnerEntity> query = context.Partners;

                if (vertical != null)
                {
                    query = query.Where(p => p.BusinessVertical == vertical);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.Name.Contains(name));
                }

                var count = await query.CountAsync();
                var partners = await query
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Include(p => p.Locations)
                    .ToListAsync();
                
                return (_mapper.Map<IReadOnlyCollection<Partner>>(partners), count);
            }
        }

        public async Task<Partner> GetByIdAsync(Guid partnerId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var entity = await context.Partners
                    .Include(p => p.Locations)
                    .FirstOrDefaultAsync(p => p.Id == partnerId);

                return _mapper.Map<Partner>(entity);
            }
        }

        public async Task<Partner> GetByClientIdAsync(string clientId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var entity = await context.Partners
                    .Include(p => p.Locations)
                    .FirstOrDefaultAsync(p => p.ClientId == clientId);

                return _mapper.Map<Partner>(entity);
            }
        }

        public async Task<Partner> GetByLocationIdAsync(Guid locationId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var entity = await context.Partners
                    .Include(p => p.Locations)
                    .FirstOrDefaultAsync(p => p.Locations.Any(l => l.Id == locationId));

                return _mapper.Map<Partner>(entity);
            }
        }

        public async Task<IReadOnlyCollection<Partner>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var partners = await context.Partners
                    .Where(p => ids.Contains(p.Id))
                    .ToListAsync();

                return _mapper.Map<IReadOnlyCollection<Partner>>(partners);
            }
        }
    }
}
