using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Tester.Web.Auth
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", true)
                .Build();

            BuildWebHost(args, config).Run();
        }

        private static IWebHost BuildWebHost(string[] args, IConfiguration configuration) =>
            WebHost.CreateDefaultBuilder(args)
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
                .UseStartup<Startup>()
                .Build();
    }
}