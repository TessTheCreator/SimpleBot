using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot1.Domain.Exceptions
{
    public class UnAuthorizedUserException : Exception 
    {
        public UnAuthorizedUserException(): base("Insufficient balance to pay for items")
        {
        }

        public UnAuthorizedUserException(string message)
            : base(message)
        {
        }

        public UnAuthorizedUserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
