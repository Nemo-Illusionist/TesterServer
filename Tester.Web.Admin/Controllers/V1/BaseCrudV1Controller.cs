using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using REST.DataCore.Contract.Entity;
using REST.Infrastructure.Contract;
using Tester.Web.Admin.Controllers.Base;

namespace Tester.Web.Admin.Controllers.V1
{
    [ApiVersion("1.0")]
    public abstract class BaseCrudV1Controller<TService, TDb, TKey, TDto, TFullDto, TRequest>
        : BaseCrudController<TService, TDb, TKey, TDto, TFullDto, TRequest>
        where TService : IBaseCrudService<TDb, TKey, TDto, TFullDto, TRequest>
        where TDb : class, IEntity<TKey>
        where TKey : IComparable
        where TDto : class
        where TFullDto : class
        where TRequest : class
    {
        protected BaseCrudV1Controller([NotNull] TService crudService, [NotNull] IFilterHelper filterHelper)
            : base(crudService, filterHelper)
        {
        }
    }
}