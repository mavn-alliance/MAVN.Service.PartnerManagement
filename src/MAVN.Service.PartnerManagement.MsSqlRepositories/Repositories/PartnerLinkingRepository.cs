using System;
using System.Threading.Tasks;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Repositories;
using MAVN.Service.PartnerManagement.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Repositories
{
    public class PartnerLinkingRepository : IPartnerLinkingRepository
    {
        private readonly PostgreSQLContextFactory<PartnerManagementContext> _contextFactory;

        public PartnerLinkingRepository(PostgreSQLContextFactory<PartnerManagementContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IPartnerLinkingInfo> GetPartnerLinkingInfoAsync(Guid partnerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.PartnersLinkingInfo.FindAsync(partnerId);

                return result;
            }
        }

        public async Task<IPartnerLinkingInfo> GetPartnerLinkingInfoByPartnerCodeAsync(string partnerCode)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.PartnersLinkingInfo
                    .FirstOrDefaultAsync(p => p.PartnerCode == partnerCode);

                return result;
            }
        }

        public async Task AddPartnerLinkingInfoAsync(IPartnerLinkingInfo model)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = PartnerLinkingInfoEntity.Create(model);

                context.PartnersLinkingInfo.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddOrUpdatePartnerLinkingInfoAsync(IPartnerLinkingInfo model)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.PartnersLinkingInfo.FindAsync(model.PartnerId);

                if (entity == null)
                {
                    entity = PartnerLinkingInfoEntity.Create(model);
                    context.PartnersLinkingInfo.Add(entity);
                }
                else
                {
                    entity.PartnerCode = model.PartnerCode;
                    entity.PartnerLinkingCode = model.PartnerLinkingCode;
                    context.PartnersLinkingInfo.Update(entity);
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> PartnerCodeExistsAsync(string partnerCode)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var codeExists = await context.PartnersLinkingInfo.AnyAsync(p => p.PartnerCode == partnerCode);

                return codeExists;
            }
        }

        public async Task<bool> CustomerHasLinkAsync(Guid customerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var hasLink = await context.LinkedPartners.AnyAsync(p => p.CustomerId == customerId);

                return hasLink;
            }
        }

        public async Task AddPartnerLinkAsync(ILinkedPartner model)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = LinkedPartnerEntity.Create(model);

                context.LinkedPartners.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Guid?> GetLinkedPartnerByCustomerIdAsync(Guid customerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var linkedPartner = await context.LinkedPartners.FindAsync(customerId);

                return linkedPartner?.PartnerId;
            }
        }
    }
}
