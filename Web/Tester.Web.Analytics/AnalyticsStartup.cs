using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tester.Auth.Extensions;
using Tester.Infrastructure.Contracts;
using Tester.Infrastructure.Services;
using Tester.Web.Core.Startup;

namespace Tester.Web.Analytics
{
    public class AnalyticsStartup : BaseStartup
    {
        public AnalyticsStartup(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment env)
            : base(loggerFactory, configuration, env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddControllers();

            var defaultApiVersion = new ApiVersion(1, 0);
            services.AddSwagger(defaultApiVersion, $"Tester Auth API {defaultApiVersion}");
            services.AddAuth(Configuration, Env);
            services.AddPostgresEf(Configuration, LoggerFactory);

            services.AddScoped<IUserTestRoService, UserTestRoService>();
            services.AddScoped<IUserAnswerRoService, UserAnswerRoService>();
        }

        public static void Configure([NotNull] IApplicationBuilder app, [NotNull] IWebHostEnvironment env,
            [NotNull] IHostApplicationLifetime lifetime, [NotNull] IServiceProvider serviceProvider,
            IApiVersionDescriptionProvider provider)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (env == null) throw new ArgumentNullException(nameof(env));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger(provider);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}