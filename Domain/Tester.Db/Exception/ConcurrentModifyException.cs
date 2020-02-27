namespace Tester.Db.Exception
{
    public class ConcurrentModifyException : System.Exception
    {
        public ConcurrentModifyException(string message) : base(message)
        {
        }

        public ConcurrentModifyException()
        {
        }

        public ConcurrentModifyException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}