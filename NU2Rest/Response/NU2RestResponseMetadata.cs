using System.Collections.Generic;

namespace NU2Rest
{
    public class NU2RestResponseMetadata
    {
        public Dictionary<KeyValuePair<string, IEnumerable<string>>, KeyValuePair<string, IEnumerable<string>>> Headers { get; set; }
        public int StatusCode { get; set; }
    }
}