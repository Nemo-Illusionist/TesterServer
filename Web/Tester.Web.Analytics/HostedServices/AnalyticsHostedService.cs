using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Tester.Db.Model.Statistics;

namespace Tester.Web.Analytics.HostedServices
{
    public class AnalyticsHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AnalyticsHostedService([NotNull] IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dataProvider = _serviceProvider.GetService<IDataProvider>();
                    var userTests = await dataProvider.GetQueryable<UserTest>()
                        .Where(x => x.IsOver)
                        .ToArrayAsync(stoppingToken);

                    var workerBlock = new ActionBlock<(UserTest, IDataProvider)>(
                        async data =>
                        {
                            try
                            {
                                await Update(data.Item1, data.Item2);
                            }
                            catch
                            {
                                // ignored
                            }
                        }, new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = -1, BoundedCapacity = -1});

                    foreach (var userTest in userTests)
                    {
                        workerBlock.Post((userTest, dataProvider));
                    }

                    workerBlock.Complete();
                    await workerBlock.Completion;
                }
            }
            catch
            {
                // ignored
            }
        }

        private async Task Update(UserTest userTest, IDataProvider dataProvider)
        {
            var userAnswers = await dataProvider.GetQueryable<UserAnswer>()
                .Where(x=>x.UserTestId == userTest.Id)
                .Include(x=>x.Question)
                .ToArrayAsync();
            
            throw new NotImplementedException();
        }
    }
}