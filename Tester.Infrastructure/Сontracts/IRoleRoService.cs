using System;
using REST.Infrastructure.Contract;
using Tester.Db.Model.Client;
using Tester.Dto;

namespace Tester.Infrastructure.Сontracts
{
    public interface IRoleRoService : IBaseRoService<Role, Guid, BaseDto<Guid>>
    {
    }
}