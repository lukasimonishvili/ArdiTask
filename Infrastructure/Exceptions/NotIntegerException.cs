namespace Infrastructure.Exceptions
{
    public class NotIntegerException : Exception
    {
        public NotIntegerException(string message) : base(message)
        {
        }
    }
}
