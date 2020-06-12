using System;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Radilovsoft.Rest.Core.Exceptions;
using Radilovsoft.Rest.Data.Core.Contract.Entity;
using Radilovsoft.Rest.Infrastructure.Contract;

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
        protected TService CrudService { get; }

        protected BaseCrudController([NotNull] TService crudService,
            [NotNull] IFilterHelper filterHelper,
            [NotNull] IValidatorFactory validatorFactory)
            : base(crudService, filterHelper, validatorFactory)
        {
            CrudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
        }


        protected virtual async Task<IActionResult> Add(TRequest item)
        {
            var id = await CrudService.Post(item).ConfigureAwait(false);
            var result = await CrudService.GetById(id).ConfigureAwait(false);
            return Ok(result);
            // ReSharper disable once Mvc.ActionNotResolved
            // return CreatedAtAction(nameof(GetById), new {id}, result);
        }

        protected virtual async Task<IActionResult> Update(TKey id, TRequest item)
        {
            try
            {
                await CrudService.Put(id, item).ConfigureAwait(false);
                return NoContent();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }

        protected virtual async Task<IActionResult> Delete(TKey id)
        {
            try
            {
                await CrudService.Delete(id).ConfigureAwait(false);
                return NoContent();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}