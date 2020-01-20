namespace Tester.Db.Exception
{
    public class ForeignKeyViolationException : System.Exception
    {
        public ForeignKeyViolationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public ForeignKeyViolationException(string message) : base(message)
        {
        }

        public ForeignKeyViolationException()
        {
        }
    }
}