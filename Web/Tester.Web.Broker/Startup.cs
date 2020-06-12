using System;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Data.Ef.Context;
using Radilovsoft.Rest.Data.Ef.Contract;
using Radilovsoft.Rest.Data.Ef.Postgres;
using Radilovsoft.Rest.Data.Ef.Provider;
using Radilovsoft.Rest.Data.Postgres;
using Tester.Auth.Extensions;
using Tester.Db.Context;
using Tester.Db.Store;

namespace Tester.Web.Broker
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup([NotNull] ILoggerFactory loggerFactory,
            [NotNull] IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
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
            services.AddApiVersioning(o =>
                {
                    o.ReportApiVersions = true;
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = defaultApiVersion;
                })
                .AddVersionedApiExplorer(o =>
                {
                    o.GroupNameFormat = "'v'VVV";
                    o.SubstituteApiVersionInUrl = true;
                })
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = $"Tester Broker API {defaultApiVersion}",
                        Version = defaultApiVersion.ToString()
                    });
                });

            services.AddAuth(_configuration, _env);

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<TesterDbContext>((sp, ob) =>
                {
                    ob.UseNpgsql(_configuration.GetConnectionString("Postgres"));
                    if (_loggerFactory != null)
                    {
                        ob.UseLoggerFactory(_loggerFactory);
                    }
                })
                .AddScoped<ResetDbContext>(x => x.GetService<TesterDbContext>())
                .AddScoped<IDataProvider, EfDataProvider>()
                .AddScoped<IRoDataProvider>(x => x.GetService<IDataProvider>())
                .AddScoped<IRwDataProvider>(x => x.GetService<IDataProvider>())
                .AddScoped<IModelStore, TesterDbModelStore>()
                .AddScoped<IAsyncHelpers, EfAsyncHelpers>()
                .AddScoped<IDataExceptionManager, PostgresDbExceptionManager>()
                .AddScoped<IIndexProvider, PostgresIndexProvider>();
            
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                foreach (var versionDescription in provider.ApiVersionDescriptions)
                {
                    o.SwaggerEndpoint($"/swagger/{(object) versionDescription.GroupName}/swagger.json",
                        versionDescription.GroupName.ToUpperInvariant());
                }

                o.EnableDeepLinking();
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}