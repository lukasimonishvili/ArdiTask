namespace Infrastructure.Exceptions
{
    public class CustomValidateException : Exception
    {
        public CustomValidateException(string message) : base(message)
        {
        }
    }
}
