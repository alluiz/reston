using System;
using System.Runtime.Serialization;

namespace RestOn
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