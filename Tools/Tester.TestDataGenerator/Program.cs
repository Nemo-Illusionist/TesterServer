using System.IO;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Mono.Options;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Data.Ef.Postgres;
using Radilovsoft.Rest.Data.Ef.Provider;
using Radilovsoft.Rest.Data.Postgres;
using Tester.Db.Context;
using Tester.Db.Store;

namespace Tester.TestDataGenerator
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
    public class Program
    {
        public static string EnvironmentName { get; private set; } = "Development";


        public static void Main(string[] args)
        {
            var dataOptions = new OptionSet
            {
                {"environment=", s => EnvironmentName = s}
            };

            dataOptions.Parse(args);

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{EnvironmentName}.json", true)
                .AddJsonFile("appsettings.local.json", true);

            var configuration = configurationBuilder.Build();
        }

        private IDataProvider GetDbContext(IConfigurationRoot configuration)
        {
            var modelStore = new TesterDbModelStore();
            var indexProvider = new PostgresIndexProvider();

            var dbContextOptions = new DbContextOptionsBuilder()
                .UseNpgsql(configuration.GetConnectionString("Postgres"),
                    o => o.MigrationsAssembly("Tester.Migrator")
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, "service"))
                .Options;

            var dbContext = new TesterDbContext(modelStore, indexProvider, dbContextOptions);
            var efDataProvider = new EfDataProvider(dbContext, new PostgresDbExceptionManager());
            return efDataProvider;
        }
    }
}