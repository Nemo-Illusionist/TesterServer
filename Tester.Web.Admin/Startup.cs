using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using REST.DataCore.Contract;
using REST.DataCore.Contract.Provider;
using REST.EfCore.Context;
using REST.EfCore.Contract;
using REST.EfCore.Provider;
using Tester.Db.Context;
using Tester.Db.Manager;
using Tester.Db.Provider;
using Tester.Db.Store;

namespace Tester.Web.Admin
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

        public void ConfigureServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}); });


            services.AddEntityFrameworkNpgsql()
                .AddDbContext<TesterDbContext>((sp, ob) =>
                {
                    ob.UseNpgsql(_configuration.GetConnectionString("Postgres"));
                    if (_loggerFactory != null)
                    {
                        ob.UseLoggerFactory(_loggerFactory);
                    }
                })
                .AddScoped<ResetDbContext>(x=>x.GetService<TesterDbContext>())
                .AddScoped<IDataProvider, EfDataProvider>()
                .AddScoped<IModelStore, TesterDbModelStore>()
                .AddScoped<IDataExceptionManager, PostgresDbExceptionManager>()
                .AddScoped<IIndexProvider, PostgresIndexProvider>();
        }

        public static void Configure([NotNull] IApplicationBuilder app, [NotNull] IWebHostEnvironment env,
            [NotNull] IHostApplicationLifetime lifetime, [NotNull] IServiceProvider serviceProvider)
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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}