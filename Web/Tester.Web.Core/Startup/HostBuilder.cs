using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Tester.Web.Core.Startup
{
    public static class HostBuilder
    {
        public static IWebHost Build<TStartup>(string[] args, IConfiguration configuration) where TStartup : class
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(configuration)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var hostingEnvironment = hostingContext.HostingEnvironment;
                    config
                        .SetBasePath(hostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true)
                        .AddJsonFile($"appsettings.local.json", true)
                        .AddEnvironmentVariables();

                    if (args == null)
                        return;

                    config.AddCommandLine(args);
                })
                .UseIISIntegration()
                .UseStartup<TStartup>()
                .Build();
        }
    }
}