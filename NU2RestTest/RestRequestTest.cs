using System;
using Xunit;
using NU2Rest;
using System.Net.Http;
using Moq;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NU2RestTest
{
    public class RestRequestTest
    {
        [Fact]
        public void TestReadAll()
        {
            /*
             * 
             * ARRANGE 
             * 
             */

            string url = "http://domain.net/test/v1/test";

            Dictionary<string, IEnumerable<string>> headers = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "read-test-1" } }
            };

            List<TestModel> dataExpected = new List<TestModel>
            {
                new TestModel() { Id = 1, Name = "ReadAllTest", Status = true },
                new TestModel() { Id = 2, Name = "ReadAllTest2", Status = false }
            };

            string body = JsonConvert.SerializeObject(dataExpected);

            const HttpStatusCode statusCode = HttpStatusCode.OK;

            IRestRequest request = CreateRestRequest(url, headers, body, statusCode);

            /*
             * 
             * ACT
             * 
             */

            RestResponse<List<TestModel>> response = request.ReadAllAsync<TestModel>().Result;

            List<TestModel> data = response.Data;
            RestResponseMetadata metadata = response.MetaData;

            /*
             * 
             * ASSERT
             * 
             */

            //Check if not null
            Assert.NotNull(data);
            Assert.NotNull(metadata);
            Assert.NotNull(request.Headers);
            Assert.NotNull(request.Params);
            Assert.NotNull(request.QueryParams);

            //Check model data
            Assert.Equal(dataExpected, data, new TestModelComparer());

            //Check metadata
            Assert.Equal((int)HttpStatusCode.OK, metadata.StatusCode);
            Assert.Equal(body, metadata.JsonBody);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);
        }

        [Fact]
        public void TestReadOne()
        {
            /*
             * 
             * ARRANGE 
             * 
             */

            string url = "http://domain.net/test/v1/test/:test_id";

            Dictionary<string, IEnumerable<string>> headers = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "read-test-1" } }
            };

            TestModel dataExpected = new TestModel() { Id = 1, Name = "ReadAllTest", Status = true };

            string body = JsonConvert.SerializeObject(dataExpected);

            const HttpStatusCode statusCode = HttpStatusCode.OK;

            IRestRequest request = CreateRestRequest(url, headers, body, statusCode);

            request.Params.Add("test_id", "1");

            /*
             * 
             * ACT
             * 
             */

            RestResponse<TestModel> response = request.ReadAsync<TestModel>().Result;

            TestModel data = response.Data;
            RestResponseMetadata metadata = response.MetaData;

            /*
             * 
             * ASSERT
             * 
             */

            //Check if not null
            Assert.NotNull(data);
            Assert.NotNull(metadata);
            Assert.NotNull(request.Headers);
            Assert.NotNull(request.Params);
            Assert.NotNull(request.QueryParams);

            //Check model data
            Assert.Equal(dataExpected, data, new TestModelComparer());

            //Check metadata
            Assert.Equal((int)HttpStatusCode.OK, metadata.StatusCode);
            Assert.Equal(body, metadata.JsonBody);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);
        }

        private static IRestRequest CreateRestRequest(string url, Dictionary<string, IEnumerable<string>> headers, string body, HttpStatusCode statusCode)
        {
            HttpClient httpClient = GetHttpClient(headers, body, statusCode);

            RestResponseEngine responseEngine = new RestResponseEngine();

            IRestRequest request = new RestRequest(url, httpClient, responseEngine);
            return request;
        }

        private static HttpClient GetHttpClient(Dictionary<string, IEnumerable<string>> headers, string READ_BODY_MOCK, HttpStatusCode statusCode)
        {
            HttpMessageHandler handler = new MockHttpMessageHandler(READ_BODY_MOCK, statusCode, headers);
            HttpClient httpClient = new HttpClient(handler);
            return httpClient;
        }
    }

    public class TestModel
    {
        public bool Status { get; set; }
        public string Name { get; set; }
        public int? Id { get; set; }
    }
}
