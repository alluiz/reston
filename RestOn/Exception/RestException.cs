using System;
using System.Runtime.Serialization;

namespace RestOn
{
    public class RestException : Exception
    {
        public RestException(string message, RestResponseMetadata response) : base(message)
        {
            Response = response;
        }

        public RestException(string message, RestResponseMetadata response, Exception innerException) : base(message, innerException)
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