using System;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Contract;
using Tester.Db.Model.Client;
using Tester.Dto;

namespace Tester.Infrastructure.Contracts
{
    public interface IRoleRoService : IBaseRoService<Role, Guid, BaseDto<Guid>>
    {
    }
}