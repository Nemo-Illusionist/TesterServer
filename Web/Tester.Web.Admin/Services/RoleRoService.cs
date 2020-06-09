using System;
using AutoMapper;
using JetBrains.Annotations;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Service;
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