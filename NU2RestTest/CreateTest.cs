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
    public class CreateTest
    {
        [Fact]
        public void TestCreate()
        {
            /*
             * 
             * ARRANGE 
             * 
             */

            string requestUrl = "http://domain.net/test/v1/test";

            Dictionary<string, IEnumerable<string>> responseHeaders = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "create-test-1" } }
            };

            TestModel dataExpected = new TestModel() { Id = 1, Name = "CreateOneTest", Status = true };

            string responseBody = JsonConvert.SerializeObject(dataExpected);

            const HttpStatusCode responseStatusCode = HttpStatusCode.Created;

            IRestRequest request = CreateMockedRestRequest(requestUrl, responseHeaders, responseBody, responseStatusCode);

            /*
             * 
             * ACT
             * 
             */

            TestModel sourceData = new TestModel() { Name = "CreateOneTeste" };

            RestResponse<TestModel> response = request.CreateAsync<TestModel, TestModel>(data: sourceData).Result;

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
            Assert.Equal((int)HttpStatusCode.Created, metadata.StatusCode);
            Assert.Equal(responseBody, metadata.JsonBody);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("create-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);
        }

        [Fact]
        public void TestReadAllWithFilters()
        {
            /*
             * 
             * ARRANGE 
             * 
             */

            string requestUrl = "http://domain.net/test/v1/test";

            Dictionary<string, IEnumerable<string>> responseHeaders = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "read-test-2" } }
            };

            List<TestModel> dataExpected = new List<TestModel>
            {
                new TestModel() { Id = 1, Name = "ReadAllTest", Status = true }
            };

            string responseBody = JsonConvert.SerializeObject(dataExpected);

            const HttpStatusCode responseStatusCode = HttpStatusCode.OK;

            IRestRequest request = CreateMockedRestRequest(requestUrl, responseHeaders, responseBody, responseStatusCode);

            /*
             * 
             * ACT
             * 
             */

            request.QueryParams.Add("name", "ReadAllTest");

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

            //Check querry params
            Assert.Single(request.QueryParams);
            Assert.Equal("ReadAllTest", request.QueryParams["name"]);

            //Check model data
            Assert.Equal(dataExpected, data, new TestModelComparer());

            //Check metadata
            Assert.Equal((int)HttpStatusCode.OK, metadata.StatusCode);
            Assert.Equal(responseBody, metadata.JsonBody);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-2", metadata.Headers["x-correlationId"].ToArray()[0]);
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

            IRestRequest request = CreateMockedRestRequest(url, headers, body, statusCode);

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

            //Check path params
            Assert.Single(request.Params);
            Assert.Equal("1", request.Params["test_id"]);

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

        private static IRestRequest CreateMockedRestRequest(string url, Dictionary<string, IEnumerable<string>> headers, string body, HttpStatusCode statusCode)
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
}
