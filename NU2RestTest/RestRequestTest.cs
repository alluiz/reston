using System;
using Xunit;
using NU2Rest;
using System.Net.Http;
using Moq;
using System.Net;
using System.Collections.Generic;
using System.Linq;

namespace NU2RestTest
{
    public class RestRequestTest
    {
        private const string READ_BODY_MOCK = "{ \"id\": 1, \"name\": \"ReadTest\", \"status\": true }";

        [Fact]
        public void TestRead()
        {
            string url = "http://domain.net/test/v1/test";

            Dictionary<string, IEnumerable<string>> headers = new Dictionary<string, IEnumerable<string>>();

            headers.Add("x-correlationId", new string[] { "read-test-1" });

            HttpMessageHandler handler = new MockHttpMessageHandler(READ_BODY_MOCK, HttpStatusCode.OK, headers);
            HttpClient httpClient = new HttpClient(handler);

            RestResponseEngine responseEngine = new RestResponseEngine();

            IRestRequest request = new RestRequest(url, httpClient, responseEngine);

            RestResponse<TestModel> response = request.ReadAsync<TestModel>().Result;

            TestModel data = response.Data;
            RestResponseMetadata metadata = response.MetaData;

            Assert.NotNull(data);
            Assert.NotNull(metadata);

            Assert.True(data.Status);
            Assert.Equal("ReadTest", data.Name);
            Assert.Equal(1, data.Id);

            Assert.Equal((int)HttpStatusCode.OK, metadata.StatusCode);
            Assert.Equal(READ_BODY_MOCK, metadata.JsonBody);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);
        }
    }

    public class TestModel
    {
        public bool Status { get; set; }
        public string Name { get; set; }
        public int? Id { get; set; }
    }
}
