using System;
using Radilovsoft.Rest.Infrastructure.Contract;
using Tester.Db.Model.Statistics;
using Tester.Dto.Statistic;

namespace Tester.Infrastructure.Contracts
{
    public interface IUserTestRoService : IBaseRoService<UserTest, Guid, UserTestDto>
    {
    }
}