using System;
using RestOn.Response;

namespace RestOn.Exception
{
    public class RestException : System.Exception
    {
        public RestException(string message, RestResponseMetadata response) : base(message)
        {
            Response = response;
        }

        public RestException(string message, RestResponseMetadata response, System.Exception innerException) : base(message, innerException)
        {
            Response = response;
        }
        
        public RestResponseMetadata Response { get; private set; }

        public override string ToString()
        {
            return
                $"{base.ToString()}{Environment.NewLine}----> ResponseMetaData in JSON format:{Environment.NewLine}{Response.ToJsonString()}";
        }
    }
}