using System;
using Radilovsoft.Rest.Infrastructure.Contract;
using Tester.Db.Model.Client;
using Tester.Dto.User;

namespace Tester.Infrastructure.Contracts
{
    public interface IUserService : IBaseCrudService<User, Guid, UserDto, UserRequest>
    {
    }
}