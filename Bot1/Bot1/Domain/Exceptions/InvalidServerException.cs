using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot1.Domain.Exceptions
{
    public class InvalidServerException : Exception
    {
        public InvalidServerException(): base("Server does not exist")
        {
        }

        public InvalidServerException(string message)
            : base(message)
        {
        }

        public InvalidServerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
