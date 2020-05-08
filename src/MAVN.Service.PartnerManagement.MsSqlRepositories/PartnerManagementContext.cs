using System.Data.Common;
using JetBrains.Annotations;
using MAVN.Common.MsSql;
using MAVN.Service.PartnerManagement.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories
{
    public class PartnerManagementContext : MsSqlContext
    {
        private const string Schema = "partner_management";

        public DbSet<PartnerEntity> Partners { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }

        public PartnerManagementContext(DbContextOptions contextOptions)
            : base(Schema, contextOptions)
        {
        }

        // C-tor for EF migrations
        [UsedImplicitly]
        public PartnerManagementContext()
            : base(Schema)
        {
        }

        public PartnerManagementContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        public PartnerManagementContext(DbContextOptions options, bool isForMocks = false)
            : base(Schema, options, isForMocks)
        {
        }

        public PartnerManagementContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        protected override void OnLykkeModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartnerEntity>()
                .OwnsMany(e => e.Locations);

            modelBuilder.Entity<PartnerEntity>()
                .HasIndex(p => p.ClientId);

            modelBuilder.Entity<PartnerEntity>()
                .HasIndex(p => p.BusinessVertical);

            modelBuilder.Entity<LocationEntity>()
                .HasIndex(p => p.ExternalId)
                .IsUnique(false);

            modelBuilder.Entity<LocationEntity>()
                .Property(x => x.AccountingIntegrationCode)
                .ValueGeneratedNever()
                .HasDefaultValue("000000")
                .HasMaxLength(80);
        }
    }
}
