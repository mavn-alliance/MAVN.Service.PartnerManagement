using AutoMapper;
using MAVN.Service.PartnerManagement.MsSqlRepositories;
using MAVN.Service.PartnerManagement.Profiles;

namespace MAVN.Service.PartnerManagement.Tests
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
