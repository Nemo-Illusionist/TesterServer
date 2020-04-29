using System;
using Tester.Dto;
using Tester.Db.Model.App;
using Tester.Dto.Tests;
using Radilovsoft.Rest.Infrastructure.Contract;

namespace Tester.Infrastructure.Contracts
{
    public interface ITestService : IBaseCrudService<Test, Guid, TestDto, TestRequest>
    {

    }
}
