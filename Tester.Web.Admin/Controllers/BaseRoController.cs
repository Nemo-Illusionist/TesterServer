using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using REST.Core.Exception;
using REST.DataCore.Contract.Entity;
using REST.Infrastructure.Contract;
using REST.Infrastructure.Contract.Dto;
using REST.Infrastructure.Dto;

namespace Tester.Web.Admin.Controllers
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

        public virtual async Task<IActionResult> GetByFilter(IPageFilter pageFilter, Filter filter, IOrder[] orders)
        {
            var expression = _filterHelper.ToExpression<TDto>(filter);
            var result = await _roService.GetByFilter(pageFilter, expression, orders).ConfigureAwait(false);
            return Ok(result);
        }

        public virtual async Task<IActionResult> GetById(TKey id)
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