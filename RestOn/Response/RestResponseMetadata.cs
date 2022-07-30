using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestOn
{
    public class RestResponseMetadata
    {
        public Dictionary<string, IEnumerable<string>> Headers { get; set; }
        public int StatusCode { get; set; }
        public string Body { get; set; }

        public string ToJsonString()
        {
            string result = JsonConvert.SerializeObject(this);

            return result;
        } 
    }
}