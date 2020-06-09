using System;
using Radilovsoft.Rest.Infrastructure.Contract;
using Tester.Db.Model.App;
using Tester.Dto.Test;

namespace Tester.Infrastructure.Contracts
{
    public interface ITestService : IBaseCrudService<Test, Guid, TestDto, TestFullDto, TestRequest>
    {
    }
}