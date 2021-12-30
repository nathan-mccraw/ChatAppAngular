using Microsoft.Extensions.DependencyInjection;

namespace API.StartUpExtensions
{
    public static class CorsServiceExtension
    {
        public static IServiceCollection AddCustomCorsExtension (this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://loclhost:4200");
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                });
            });

            return services;
        }
    }
}
