using Npgsql;
using REST.DataCore.Contract;
using Tester.Db.Exception;

namespace Tester.Db.Provider
{
    public class PostgresDbExceptionManager : IDataExceptionManager
    {
        public System.Exception Normalize(System.Exception exception)
        {
            if (exception != null && exception.InnerException is PostgresException ex)
            {
                var message = ex.Message + ex.Detail;

                switch (ex.SqlState)
                {
                    case "23503":
                        throw new ForeignKeyViolationException(message, ex);
                    case "23505":
                        throw new ObjectAlreadyExistsException(message, ex);
                    case "40001":
                        throw new ConcurrentModifyException(message, ex);
                    default:
                        throw ex;
                }
            }

            return exception;
        }

        public bool IsConcurrentModifyException(System.Exception ex)
        {
            return ex is ConcurrentModifyException;
        }
    }
}