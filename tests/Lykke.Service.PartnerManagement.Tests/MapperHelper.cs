using AutoMapper;
using Lykke.Service.PartnerManagement.MsSqlRepositories;
using Lykke.Service.PartnerManagement.Profiles;

namespace Lykke.Service.PartnerManagement.Tests
{
    public static class MapperHelper
    {
        public static IMapper CreateAutoMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(ServiceProfile), typeof(AutoMapperProfile)));

            return config.CreateMapper();
        }
    }
}
