using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Tester.Auth.Models;
using Tester.Auth.Services;

namespace Tester.Auth.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAuth([NotNull] this IServiceCollection services,
            [NotNull] IConfiguration configuration, [NotNull] IHostEnvironment env)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (env == null) throw new ArgumentNullException(nameof(env));

            var authSettingsSection = configuration.GetSection(nameof(AuthOptions));
            services.Configure<AuthOptions>(authSettingsSection);
            var authOptions = authSettingsSection.Get<AuthOptions>();

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.Secret));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            services.Configure<TokenAuthOptions>(options =>
            {
                options.Audience = $"TesterAudience_{env.EnvironmentName}";
                options.Issuer = $"TesterIssuer_{env.EnvironmentName}";
                options.SigningCredentials = signingCredentials;
                options.Expiration = TimeSpan.FromDays(30);
            });
            var tokenAuthOptions = services.BuildServiceProvider().GetService<IOptions<TokenAuthOptions>>().Value;
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = tokenAuthOptions.SigningCredentials.Key,
                ValidateIssuer = true,
                ValidIssuer = tokenAuthOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = tokenAuthOptions.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(co =>
                {
                    co.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = ctx => CheckIsApiAndReturnStatus(ctx, HttpStatusCode.Unauthorized),
                        OnRedirectToAccessDenied = ctx => CheckIsApiAndReturnStatus(ctx, HttpStatusCode.Forbidden)
                    };
                    co.Cookie.Name = "access_token";
                    co.Cookie.HttpOnly = true;
                    co.TicketDataFormat = new JwtDataFormat(SecurityAlgorithms.HmacSha256, tokenValidationParameters);
                });
            return services;
        }

        private static Task CheckIsApiAndReturnStatus(RedirectContext<CookieAuthenticationOptions> ctx,
            HttpStatusCode statusCode)
        {
            if (ctx.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase) &&
                ctx.Response.StatusCode == (int) HttpStatusCode.OK)
            {
                ctx.Response.StatusCode = (int) statusCode;
            }
            else
            {
                ctx.Response.Redirect(ctx.RedirectUri);
            }

            return Task.FromResult(0);
        }
    }
}