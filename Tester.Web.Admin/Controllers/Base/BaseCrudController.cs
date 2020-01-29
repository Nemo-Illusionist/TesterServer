using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using REST.Core.Exception;
using REST.DataCore.Contract.Entity;
using REST.Infrastructure.Contract;

namespace Tester.Web.Admin.Controllers.Base
{
    public abstract class BaseCrudController<TService, TDb, TKey, TDto, TFullDto, TRequest>
        : BaseRoController<TService, TDb, TKey, TDto, TFullDto>
        where TService : IBaseCrudService<TDb, TKey, TDto, TFullDto, TRequest>
        where TDb : class, IEntity<TKey>
        where TKey : IComparable
        where TDto : class
        where TFullDto : class
        where TRequest : class
    {
        private readonly TService _crudService;

        protected BaseCrudController([NotNull] TService crudService,
            [NotNull] IFilterHelper filterHelper)
            : base(crudService, filterHelper)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
        }


        public virtual async Task<IActionResult> Add(TRequest item)
        {
            var id = await _crudService.Post(item).ConfigureAwait(false);
            var result = await _crudService.GetById(id).ConfigureAwait(false);
            // ReSharper disable once Mvc.ActionNotResolved
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