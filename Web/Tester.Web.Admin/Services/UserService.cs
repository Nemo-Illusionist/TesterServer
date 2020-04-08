using Radilovsoft.Rest.Infrastructure.Contract.Dto;
using Radilovsoft.Rest.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tester.Infrastructure.Contracts;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Service;
using Tester.Dto.Users;
using Tester.Db.Model.Client;
using AutoMapper;

namespace Tester.Web.Admin.Services
{
    public class UserService : BaseRoService<User, Guid, UserDto, UserDto>, IUserService

    {
        public UserService(IRoDataProvider dataProvider, IAsyncHelpers asyncHelpers, IOrderHelper orderHelper, IMapper mapper) 
            : base(dataProvider, asyncHelpers, orderHelper, mapper)
        {
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

       
        public Task<Guid> Post(UserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Put(Guid id, UserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
