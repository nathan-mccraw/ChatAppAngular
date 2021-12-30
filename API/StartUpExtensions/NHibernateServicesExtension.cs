using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.NetCore;
using System;

namespace API.StartUpExtensions
{
    public static class NHibernateServicesExtension
    {
        public static IServiceCollection AddCustomNHibernateExtension (this IServiceCollection services, IConfiguration _configuration)
        {
            var path = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "nhibernate.cfg.xml"
                );
            var connString = _configuration["ConnectionStrings:Default"];
            var config = new NHibernate.Cfg.Configuration().Configure(path);
            config.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connString);
            services.AddHibernate(config);

            return services;
        }
    }
}
