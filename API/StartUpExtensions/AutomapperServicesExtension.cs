using API.Helpers;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace API.StartUpExtensions
{
    public static class AutomapperServicesExtension
    {
        public static IServiceCollection AddCustomAutomapperExtension (this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfiles());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
