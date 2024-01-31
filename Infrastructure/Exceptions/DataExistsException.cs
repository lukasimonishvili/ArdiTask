namespace Infrastructure.Exceptions
{
    public class DataExistsException : Exception
    {
        public DataExistsException(string message) : base(message)
        {
        }
    }
}
