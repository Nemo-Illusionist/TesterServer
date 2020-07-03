using System;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Service;
using Tester.Auth.Extensions;
using Tester.Infrastructure.Contracts;
using Tester.Infrastructure.Services;
using Tester.Web.Core.Startup;

namespace Tester.Web.Broker
{
    public class BrokerStartup : BaseStartup
    {
        public BrokerStartup(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment env)
            : base(loggerFactory, configuration, env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddControllers()
                .AddFluentValidation(config =>
                {
                    config.LocalizationEnabled = true;
                    config.RegisterValidatorsFromAssembly(GetType().Assembly);
                });

            var defaultApiVersion = new ApiVersion(1, 0);
            services.AddSwagger(defaultApiVersion, $"Tester Broker API {defaultApiVersion}");
            services.AddAuth(Configuration, Env);
            services.AddPostgresEf(Configuration, LoggerFactory);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMemoryCache();

            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IFilterHelper, FilterHelper>();
            services.AddScoped<IOrderHelper, OrderHelper>();
            services.AddScoped<IExpressionHelper, ExpressionHelper>();
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