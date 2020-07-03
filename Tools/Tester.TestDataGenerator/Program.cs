using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Mono.Options;
using Radilovsoft.Rest.Data.Ef.Context;
using Radilovsoft.Rest.Data.Ef.Postgres;
using Radilovsoft.Rest.Data.Ef.Provider;
using Radilovsoft.Rest.Data.Postgres;
using Tester.Db.Context;
using Tester.Db.Store;
using Tester.TestDataGenerator.DataGenerator;

namespace Tester.TestDataGenerator
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
    public static class Program
    {
        private static string EnvironmentName { get; set; } = "Development";

        public static async Task Main(string[] args)
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

            await using var dbContext = GetDbContext(configuration);
            var dataProvider = new EfDataProvider(dbContext, new PostgresDbExceptionManager());
            await using var transaction = dataProvider.Transaction();
            
            var users = await UserGenerator.Gen(dataProvider).ConfigureAwait(false);
            Console.WriteLine("users: {0}", users.Count());
            
            var topics = await TopicGenerator.Gen(dataProvider, users).ConfigureAwait(false);
            Console.WriteLine("topics: {0}", topics.Count());

            var tests = await TestGenerator.Gen(dataProvider, users, topics).ConfigureAwait(false);
            Console.WriteLine("tests: {0}", tests.Count());

            var questions = await QuestionGenerator.Gen(dataProvider, users, topics).ConfigureAwait(false);
            Console.WriteLine("questions: {0}", questions.Count());

            await transaction.CommitAsync().ConfigureAwait(false);
        }

        private static ResetDbContext GetDbContext(IConfigurationRoot configuration)
        {
            var modelStore = new TesterDbModelStore();
            var indexProvider = new PostgresIndexProvider();

            var dbContextOptions = new DbContextOptionsBuilder()
                .UseNpgsql(configuration.GetConnectionString("Postgres"),
                    o => o.MigrationsAssembly("Tester.Migrator")
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, "service"))
                .Options;

            return new TesterDbContext(modelStore, indexProvider, dbContextOptions);
        }
    }
}