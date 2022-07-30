using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RestOn.Tests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string jsonBodyResponse;
        private readonly HttpStatusCode statusCode;
        private readonly Dictionary<string, IEnumerable<string>> headers;

        public MockHttpMessageHandler(string jsonBodyResponse, HttpStatusCode statusCode, Dictionary<string, IEnumerable<string>> headers = null)
        {
            this.jsonBodyResponse = jsonBodyResponse;
            this.statusCode = statusCode;
            this.headers = headers;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {

            var responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(jsonBodyResponse)
            };

            if (headers != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
                {
                    responseMessage.Headers.Add(header.Key, header.Value);
                }
            }

            return await Task.FromResult(responseMessage);
        }
    }
}