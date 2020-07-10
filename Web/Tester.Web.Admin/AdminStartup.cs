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
using Tester.Auth.Contracts;
using Tester.Auth.Extensions;
using Tester.Auth.Services;
using Tester.Infrastructure.Contracts;
using Tester.Infrastructure.Services;
using Tester.Web.Core.Startup;

namespace Tester.Web.Admin
{
    public class AdminStartup : BaseStartup
    {
        public AdminStartup(ILoggerFactory loggerFactory, IConfiguration configuration, IWebHostEnvironment env)
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
            services.AddSwagger(defaultApiVersion, $"Tester Admin API {defaultApiVersion}");
            services.AddAuth(Configuration, Env);
            services.AddPostgresEf(Configuration, LoggerFactory);

            services.AddScoped<IRoleRoService, RoleRoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFilterHelper, FilterHelper>();
            services.AddScoped<IExpressionHelper, ExpressionHelper>();
            services.AddScoped<IOrderHelper, OrderHelper>();
            services.AddScoped<IPasswordProvider, PasswordProvider>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<ITestTopicService, TestTopicService>();

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
            app.UseSwagger(provider);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}