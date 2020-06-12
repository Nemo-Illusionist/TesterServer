using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Radilovsoft.Rest.Data.Ef.Contract;

namespace Tester.Db.Store
{
    public class TesterDbModelStore : IModelStore
    {
        private readonly Type[] _modelTypes;

        public TesterDbModelStore()
        {
            _modelTypes = (typeof(TesterDbModelStore).Assembly.GetExportedTypes())
                .Where(x => !x.IsInterface && x.GetCustomAttribute<TableAttribute>() != null)
                .ToArray();
        }

        public IEnumerable<Type> GetModels()
        {
            return _modelTypes;
        }
    }
}