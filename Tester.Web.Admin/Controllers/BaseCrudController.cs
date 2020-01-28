using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morcatko.AspNetCore.JsonMergePatch;
using REST.Core.Exception;
using REST.DataCore.Contract.Entity;
using REST.Infrastructure.Contract;
using REST.Infrastructure.Contract.Dto;
using REST.Infrastructure.Dto;

namespace Tester.Web.Admin.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/admin/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseCrudController<TService, TDb, TKey, TDto, TFullDto, TRequest> : Controller
        where TService : IBaseCrudService<TDb, TKey, TDto, TFullDto, TRequest>
        where TDb : class, IEntity<TKey>
        where TKey : IComparable
        where TDto : class
        where TFullDto : class
        where TRequest : class
    {
        private readonly TService _crudService;
        private readonly IFilterHelper _filterHelper;

        public BaseCrudController([NotNull] TService crudService,
            [NotNull] IFilterHelper filterHelper)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
            _filterHelper = filterHelper ?? throw new ArgumentNullException(nameof(filterHelper));
        }

        public virtual async Task<IActionResult> GetByFilter(IPageFilter pageFilter, Filter filter, IOrder[] orders)
        {
            var expression = _filterHelper.ToExpression<TDto>(filter);
            var result = await _crudService.GetByFilter(pageFilter, expression, orders).ConfigureAwait(false);
            return Ok(result);
        }

        public virtual async Task<IActionResult> GetById(TKey id)
        {
            try
            {
                var result = await _crudService.GetById(id).ConfigureAwait(false);
                return Ok(result);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }

        public virtual async Task<IActionResult> Add(TRequest item)
        {
            var id = await _crudService.Post(item).ConfigureAwait(false);
            var result = await _crudService.GetById(id).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetById), new {id}, result);
        }

        public virtual async Task<IActionResult> Update(TKey id, [FromBody] TRequest item)
        {
            try
            {
                await _crudService.Put(id, item).ConfigureAwait(false);
                return NoContent();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }

        public virtual async Task<IActionResult> HalfUpdate(TKey id, JsonMergePatchDocument<TRequest> item)
        {
            try
            {
                await _crudService.Patch(id, item).ConfigureAwait(false);
                return NoContent();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }

        public virtual async Task<IActionResult> Delete(TKey id)
        {
            try
            {
                await _crudService.Delete(id).ConfigureAwait(false);
                return NoContent();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}