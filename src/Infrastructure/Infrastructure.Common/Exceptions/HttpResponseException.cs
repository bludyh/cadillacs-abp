using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(string message, int status = 500) : base(message) {
            Status = status;
        }

        public int Status { get; }
    }
}
