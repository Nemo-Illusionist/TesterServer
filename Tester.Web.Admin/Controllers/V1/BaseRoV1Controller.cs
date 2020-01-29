using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REST.DataCore.Contract.Entity;
using REST.Infrastructure.Contract;

namespace Tester.Web.Admin.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    public abstract class BaseRoV1Controller<TService, TDb, TKey, TDto, TFullDto>
        : BaseRoController<TService, TDb, TKey, TDto, TFullDto>
        where TService : IBaseRoService<TDb, TKey, TDto, TFullDto>
        where TDb : class, IEntity<TKey>
        where TKey : IComparable
        where TDto : class
        where TFullDto : class
    {
        protected BaseRoV1Controller([NotNull] TService crudService, [NotNull] IFilterHelper filterHelper)
            : base(crudService, filterHelper)
        {
        }
    }
}