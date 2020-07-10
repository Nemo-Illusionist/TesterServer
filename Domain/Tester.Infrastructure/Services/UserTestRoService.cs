using System;
using AutoMapper;
using JetBrains.Annotations;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Service;
using Tester.Db.Model.Statistics;
using Tester.Dto.Statistic;
using Tester.Infrastructure.Contracts;

namespace Tester.Infrastructure.Services
{
    public class UserTestRoService : BaseRoService<UserTest, Guid, UserTestDto, UserTestDto>, IUserTestRoService
    {
        public UserTestRoService([NotNull] IRoDataProvider dataProvider,
            [NotNull] IAsyncHelpers asyncHelpers,
            [NotNull] IOrderHelper orderHelper,
            [NotNull] IMapper mapper)
            : base(dataProvider, asyncHelpers, orderHelper, mapper)
        {
        }
    }
}