using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Data.Ef.Context;
using Radilovsoft.Rest.Data.Ef.Contract;
using Radilovsoft.Rest.Data.Ef.Postgres;
using Radilovsoft.Rest.Data.Ef.Provider;
using Radilovsoft.Rest.Data.Postgres;
using Tester.Db.Context;
using Tester.Db.Store;

namespace Tester.Web.Core.Startup
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services,
            ApiVersion defaultApiVersion, string title)
        {
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
                        Title = title,
                        Version = defaultApiVersion.ToString()
                    });
                });
            return services;
        }

        public static IServiceCollection AddPostgresEf(this IServiceCollection services, IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            services
                // .AddEntityFrameworkNpgsql()
                .AddDbContext<TesterDbContext>((sp, ob) =>
                {
                    ob.UseNpgsql(configuration.GetConnectionString("Postgres"));
                    if (loggerFactory != null)
                    {
                        ob.UseLoggerFactory(loggerFactory);
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

            return services;
        }
    }
}