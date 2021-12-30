using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Hub;
using API.StartUpExtensions;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomAutomapperExtension();

            services.AddCustomJwtAuthExtension(_configuration);

            services.AddCustomNHibernateExtension(_configuration);

            //services.AddCustomCorsExtension();

            services.AddCustomSwaggerExtension();

            services.AddCustomDIExtension();

            services.AddControllers();

            services.AddSignalR();
            
            services.AddCookiePolicy(options =>
            {
                options.ConsentCookie.HttpOnly = true;
            });

            services.AddSpaStaticFiles(config =>
                config.RootPath = "angularClient/dist");

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.Audience = _configuration["AAD:ResourceId"];
            //        options.Authority = $"{_configuration["AAD:InstanceId"]}{_configuration["AAD:TentantId"]}";
            //    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            //app.UseCors();

            //app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSpaStaticFiles();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}"
                    );
                endpoints.MapHub<ChatHub>("/chathub");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "angularClient";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer("start");
                }
            });
        }
    }
}