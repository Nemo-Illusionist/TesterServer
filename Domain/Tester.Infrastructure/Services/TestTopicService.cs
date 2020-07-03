using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Contract.Dto;
using Radilovsoft.Rest.Infrastructure.Dto;
using Radilovsoft.Rest.Infrastructure.Extension;
using Tester.Db.Model.App;
using Tester.Dto.TestTopic;
using Tester.Infrastructure.Contracts;

namespace Tester.Infrastructure.Services
{
    public class TestTopicService : ITestTopicService
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMapper _mapper;
        private readonly IAsyncHelpers _asyncHelpers;
        private readonly IOrderHelper _orderHelper;

        public TestTopicService([NotNull] IDataProvider dataProvider,
            [NotNull] IMapper mapper,
            [NotNull] IAsyncHelpers asyncHelpers,
            [NotNull] IOrderHelper orderHelper)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _asyncHelpers = asyncHelpers ?? throw new ArgumentNullException(nameof(asyncHelpers));
            _orderHelper = orderHelper ?? throw new ArgumentNullException(nameof(orderHelper));
        }

        public async Task<PagedResult<TestTopicDto>> GetByFilter(IPageFilter pageFilter,
            Expression<Func<TestTopicDto, bool>> filter = null, IOrder[] orders = null)
        {
            if (pageFilter == null) throw new ArgumentNullException(nameof(pageFilter));

            var queryable = GetQueryable(pageFilter, filter, orders, false);
            var queryableForCount = GetQueryable(pageFilter, filter, orders, true);

            return new PagedResult<TestTopicDto>
            {
                Data = await _asyncHelpers.ToArrayAsync(queryable).ConfigureAwait(false),
                Meta = new Meta
                {
                    Page = pageFilter.Page,
                    PageSize = pageFilter.PageSize,
                    Count = await _asyncHelpers.LongCountAsync(queryableForCount).ConfigureAwait(false)
                }
            };
        }

        public async Task Post(TestTopicRequest request)
        {
            var db = _mapper.Map<TestTopic>(request);
            await _dataProvider.InsertAsync(db).ConfigureAwait(false);
        }

        public async Task Deleted(Guid testId, Guid topicId)
        {
            var tt = await _dataProvider.GetQueryable<TestTopic>()
                .Where(x => x.TestId == testId && x.TopicId == topicId)
                .SingleAsync().ConfigureAwait(false);
            tt.DeletedUtc = DateTime.UtcNow;
            await _dataProvider.UpdateAsync(tt).ConfigureAwait(false);
        }

        private IQueryable<TestTopicDto> GetQueryable(IPageFilter pageFilter,
            Expression<Func<TestTopicDto, bool>> filter,
            IOrder[] orders, bool isCount)
        {
            var queryable = _dataProvider.GetQueryable<TestTopic>().ProjectTo<TestTopicDto>(_mapper);

            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }

            if (!isCount)
            {
                if (orders != null && orders.Any())
                {
                    queryable = _orderHelper.ApplyOrderBy(queryable, orders);
                }

                if (pageFilter != null)
                {
                    queryable = queryable.FilterPage(pageFilter);
                }
            }

            return queryable;
        }
    }
}