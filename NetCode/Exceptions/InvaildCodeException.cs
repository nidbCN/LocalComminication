using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCode.Exceptions
{
    public class InvaildCodeException : Exception
    {
        public InvaildCodeException()
        {
        }

        public InvaildCodeException(string code) : base($"NetCode {code} is invaild.")
        {
        }

        public InvaildCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
