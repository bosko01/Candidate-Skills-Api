using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class ErrorDetails
    {
        public HttpStatusCode StatusCode { get; private set; }

        public string Message { get; private set; }

        private ErrorDetails(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public static ErrorDetails Create(HttpStatusCode statusCode, string message)
        {
            return new ErrorDetails(statusCode, message);
        }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}