namespace Bot1.Domain.Exceptions
{
    public class InvalidEventException : Exception
    {
        public InvalidEventException(): base("Event does not exist")
        {
        }

        public InvalidEventException(string message)
            : base(message)
        {
        }

        public InvalidEventException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
