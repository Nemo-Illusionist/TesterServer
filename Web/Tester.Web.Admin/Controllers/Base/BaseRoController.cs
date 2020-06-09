using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Core.Exceptions;
using Radilovsoft.Rest.Data.Core.Contract.Entity;
using Radilovsoft.Rest.Infrastructure.Contract;
using Radilovsoft.Rest.Infrastructure.Contract.Dto;
using Tester.Dto;
using Tester.Web.Admin.Models;

namespace Tester.Web.Admin.Controllers.Base
{
    public abstract class BaseRoController<TService, TDb, TKey, TDto, TFullDto> : BaseController
        where TService : IBaseRoService<TDb, TKey, TDto, TFullDto>
        where TDb : class, IEntity<TKey>
        where TKey : IComparable
        where TDto : class
        where TFullDto : class
    {
        protected  IFilterHelper FilterHelper {get;}
        protected  TService RoService {get;}

        protected BaseRoController([NotNull] TService roService, [NotNull] IFilterHelper filterHelper)
        {
            RoService = roService ?? throw new ArgumentNullException(nameof(roService));
            FilterHelper = filterHelper ?? throw new ArgumentNullException(nameof(filterHelper));
        }

        protected async Task<IActionResult> GetByFilter(FilterRequest filter)
        {
            Expression<Func<TDto, bool>> expression = null;
            IPageFilter pageFilter = null;
            IOrder[] orders = null;
            if (filter != null)
            {
                if (filter.Filter != null)
                {
                    expression = FilterHelper.ToExpression<TDto>(filter.Filter);
                }

                pageFilter = filter.PageFilter ?? new PageFilter {Page = 1, PageSize = 20};
                orders = filter.Orders?.ToArray();
            }

            var result = await RoService.GetByFilter(pageFilter, expression, orders)
                .ConfigureAwait(false);
            return Ok(result);
        }

        protected async Task<IActionResult> GetById(TKey id)
        {
            try
            {
                var result = await RoService.GetById(id).ConfigureAwait(false);
                return Ok(result);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}