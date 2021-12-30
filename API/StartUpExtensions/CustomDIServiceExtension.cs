using API.Authentication;
using Core.Interfaces;
using Core.Services;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace API.StartUpExtensions
{
    public static class CustomDIServiceExtension
    {
        public static IServiceCollection AddCustomDIExtension (this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserSessionService, UserSessionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            return services;
        }
    }
}
