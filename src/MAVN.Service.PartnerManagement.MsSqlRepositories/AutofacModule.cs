using Autofac;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.PartnerManagement.Domain.Repositories;
using MAVN.Service.PartnerManagement.MsSqlRepositories.Repositories;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories
{
    public class AutofacModule: Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterPostgreSQL(
                _connectionString,
                connString => new PartnerManagementContext(connString, false),
                dbConn => new PartnerManagementContext(dbConn));

            builder.RegisterType<PartnerRepository>()
                .As<IPartnerRepository>()
                .SingleInstance();

            builder.RegisterType<LocationRepository>()
                .As<ILocationRepository>()
                .SingleInstance();

            builder.RegisterType<PartnerLinkingRepository>()
                .As<IPartnerLinkingRepository>()
                .SingleInstance();
        }
    }
}
