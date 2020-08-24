using System;
using System.Runtime.Serialization;

namespace NU2Rest
{
    public class RestAuthenticationException : RestException
    {
        public RestAuthenticationException(string message, RestResponseMetadata response) : base(message, response)
        {
        }

        public RestAuthenticationException(string message, RestResponseMetadata response, Exception innerException) : base(message, response, innerException)
        {
        }
    }
}