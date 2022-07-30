using System;
using Xunit;
using RestOn;
using System.Net.Http;
using Moq;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestOn.Http;

namespace RestOn.Tests
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

            string requestUri = "http://domain.net/test/v1/test";
            string requestUriExpected = "http://domain.net/test/v1/test";

            Dictionary<string, IEnumerable<string>> responseHeaders = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "create-test-1" } }
            };

            TestModel sourceData = new TestModel() { Name = "CreateOneTeste" };
            TestModel dataExpected = new TestModel() { Id = 1, Name = "CreateOneTest", Status = true };

            string responseBody = JsonConvert.SerializeObject(dataExpected);

            const HttpStatusCode responseStatusCode = HttpStatusCode.Created;

            Mock<IHttpClientDecorator> mockHttpClient = MockRequest.GetMockHttpClientDecorator(new Uri(requestUriExpected), HttpMethod.Post, responseHeaders, responseBody, responseStatusCode);

            IRestRequest request = MockRequest.GetRestRequest(requestUri, mockHttpClient.Object);

            /*
             * 
             * ACT
             * 
             */

            RestResponse<TestModel> response = request.PostAsync<TestModel, TestModel>(data: sourceData).Result;

            TestModel data = response.Data;
            RestResponseMetadata metadata = response.Metadata;

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
            Assert.Equal(responseBody, metadata.Body);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("create-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);
        }


        [Fact]
        public void TestCreateWithQueryParam()
        {
            /*
             * 
             * ARRANGE 
             * 
             */

            string requestUri = "http://domain.net/test/v1/test";
            string requestUriExpected = "http://domain.net/test/v1/test?response_format=json";

            Dictionary<string, IEnumerable<string>> responseHeaders = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "create-test-2" } }
            };

            TestModel dataExpected = new TestModel() { Id = 1, Name = "CreateOneTest", Status = true };

            string responseBody = JsonConvert.SerializeObject(dataExpected);

            const HttpStatusCode responseStatusCode = HttpStatusCode.Created;

            Mock<IHttpClientDecorator> mockHttpClient = MockRequest.GetMockHttpClientDecorator(new Uri(requestUriExpected), HttpMethod.Post, responseHeaders, responseBody, responseStatusCode);

            IRestRequest request = MockRequest.GetRestRequest(requestUri, mockHttpClient.Object);

            /*
             * 
             * ACT
             * 
             */

            request.QueryParams.Add("response_format", "json");

            TestModel sourceData = new TestModel() { Name = "CreateOneTeste" };

            RestResponse<TestModel> response = request.PostAsync<TestModel, TestModel>(sourceData).Result;

            TestModel data = response.Data;
            RestResponseMetadata metadata = response.Metadata;

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
            Assert.Equal(responseBody, metadata.Body);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("create-test-2", metadata.Headers["x-correlationId"].ToArray()[0]);
        }
    }
}
