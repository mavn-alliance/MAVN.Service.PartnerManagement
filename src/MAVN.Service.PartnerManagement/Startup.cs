using System;
using AutoMapper;
using JetBrains.Annotations;
using Lykke.Sdk;
using MAVN.Service.PartnerManagement.MsSqlRepositories;
using MAVN.Service.PartnerManagement.Profiles;
using MAVN.Service.PartnerManagement.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MAVN.Service.PartnerManagement
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "PartnerManagement API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.Extend = (sc, st) =>
                {
                    sc.AddAutoMapper(typeof(ServiceProfile), typeof(AutoMapperProfile));
                    sc.AddHttpClient();
                };

                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "PartnerManagementLog";
                    logs.AzureTableConnectionStringResolver =
                        settings => settings.PartnerManagementService.Db.LogsConnString;
                };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IMapper mapper)
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            app.UseLykkeConfiguration(options => { options.SwaggerOptions = _swaggerOptions; });
        }
    }
}
