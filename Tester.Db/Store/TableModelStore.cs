using REST.EfCore.Provider;

namespace Tester.Db.Store
{
    public class TesterDbModelStore : EntityModelStore
    {
        public TesterDbModelStore() : base(typeof(TesterDbModelStore).Assembly)
        {
        }
    }
}