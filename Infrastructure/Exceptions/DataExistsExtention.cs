namespace Infrastructure.Exceptions
{
    public class DataExistsExtention : Exception
    {
        public DataExistsExtention(string message) : base(message)
        {
        }
    }
}
