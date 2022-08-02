using RestOn.Response;

namespace RestOn.Exception
{
    public class RestAuthenticationException : RestException
    {
        public RestAuthenticationException(string message, RestResponseMetadata response) : base(message, response)
        {
        }

        public RestAuthenticationException(string message, RestResponseMetadata response, System.Exception innerException) : base(message, response, innerException)
        {
        }
    }
}