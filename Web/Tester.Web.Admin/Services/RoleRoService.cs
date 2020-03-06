using System;
using AutoMapper;
using JetBrains.Annotations;
using REST.DataCore.Contract;
using REST.DataCore.Contract.Provider;
using REST.Infrastructure.Contract;
using REST.Infrastructure.Service;
using Tester.Db.Model.Client;
using Tester.Dto;
using Tester.Infrastructure.Contracts;

namespace Tester.Web.Admin.Services
{
    public class RoleRoService : BaseRoService<Role, Guid, BaseDto<Guid>, BaseDto<Guid>>, IRoleRoService
    {
        public RoleRoService([NotNull] IRoDataProvider dataProvider,
            [NotNull] IAsyncHelpers asyncHelpers,
            [NotNull] IOrderHelper orderHelper,
            [NotNull] IMapper mapper) : base(dataProvider, asyncHelpers, orderHelper, mapper)
        {
        }
    }
}