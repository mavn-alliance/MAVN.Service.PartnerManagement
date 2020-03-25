using Autofac;
using Lykke.Common.MsSql;
using Lykke.Service.PartnerManagement.Domain.Repositories;
using Lykke.Service.PartnerManagement.MsSqlRepositories.Repositories;

namespace Lykke.Service.PartnerManagement.MsSqlRepositories
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
            builder.RegisterMsSql(
                _connectionString,
                connString => new PartnerManagementContext(connString, false),
                dbConn => new PartnerManagementContext(dbConn));

            builder.RegisterType<PartnerRepository>()
                .As<IPartnerRepository>()
                .SingleInstance();

            builder.RegisterType<LocationRepository>()
                .As<ILocationRepository>()
                .SingleInstance();
        }
    }
}
