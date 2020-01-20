using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REST.EfCore.Contract;

namespace Tester.Db.Provider
{
    public class PostgresIndexProvider : IIndexProvider
    {
        public IndexBuilder HasMethod(IndexBuilder indexBuilder, string attributeMethod)
        {
            return indexBuilder.HasMethod(attributeMethod);
        }
    }
}