using Radilovsoft.Rest.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Tester.Db.Model.Client;
using Tester.Dto.Users;

namespace Tester.Infrastructure.Contracts
{
   public interface IUserService: IBaseCrudService<User, Guid, UserDto, UserRequest>

    {

    }
}
