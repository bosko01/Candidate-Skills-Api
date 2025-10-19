using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class CustomException : System.Exception
    {
        protected CustomException() : base()
        {
        }

        protected CustomException(string message) : base(message)
        {
        }

        protected CustomException(string message, System.Exception exception) : base(message, exception)
        {
        }
    }
}
