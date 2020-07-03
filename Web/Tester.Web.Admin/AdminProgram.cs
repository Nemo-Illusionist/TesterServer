using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tester.Web.Core.Startup;

namespace Tester.Web.Admin
{
    public static class AdminProgram
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();
            
            HostBuilder.Build<AdminStartup>(args, config).Run();
        }
    }
}