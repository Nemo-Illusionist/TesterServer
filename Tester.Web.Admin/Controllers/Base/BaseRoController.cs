using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using REST.Core.Exception;
using REST.DataCore.Contract.Entity;
using REST.Infrastructure.Contract;
using REST.Infrastructure.Contract.Dto;
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
        private readonly IFilterHelper _filterHelper;
        private readonly TService _roService;

        protected BaseRoController([NotNull] TService roService, [NotNull] IFilterHelper filterHelper)
        {
            _roService = roService ?? throw new ArgumentNullException(nameof(roService));
            _filterHelper = filterHelper ?? throw new ArgumentNullException(nameof(filterHelper));
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
                    expression = _filterHelper.ToExpression<TDto>(filter.Filter);
                }

                pageFilter = filter.PageFilter ?? new PageFilter {Page = 1, PageSize = 20};
                orders = filter.Orders;
            }

            var result = await _roService.GetByFilter(pageFilter, expression, orders)
                .ConfigureAwait(false);
            return Ok(result);
        }

        protected async Task<IActionResult> GetById(TKey id)
        {
            try
            {
                var result = await _roService.GetById(id).ConfigureAwait(false);
                return Ok(result);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}