using System;
using Xunit;
using NU2Rest;
using System.Net.Http;
using Moq;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace NU2RestTest
{
    public class ReadTest
    {
        [Fact]
        public void TestReadAll()
        {
            /*
             * 
             * ARRANGE 
             * 
             */

            string requestUrl = "http://domain.net/test/v1/test";

            Dictionary<string, IEnumerable<string>> responseHeaders = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "read-test-1" } }
            };

            List<TestModel> dataExpected = new List<TestModel>
            {
                new TestModel() { Id = 1, Name = "ReadAllTest", Status = true },
                new TestModel() { Id = 2, Name = "ReadAllTest2", Status = false }
            };

            string responseBody = JsonConvert.SerializeObject(dataExpected);

            const HttpStatusCode responseStatusCode = HttpStatusCode.OK;

            Mock<IHttpClientDecorator> mockHttpClient = MockRequest.GetMockHttpClientDecorator(HttpMethod.Get, responseHeaders, responseBody, responseStatusCode);

            IRestRequest request = MockRequest.GetRestRequest(requestUrl, mockHttpClient.Object);

            request.Headers.Add("x-mock-apikey", new string[]{ "apikey-test" });
            request.UseBasicAuthentication(MockRequest.USERNAME, MockRequest.PASSWORD);

            /*
             * 
             * ACT
             * 
             */

            RestResponse<List<TestModel>> response = request.ReadListAsync<TestModel>().Result;

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
            Assert.Equal(responseBody, metadata.JsonBody);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);

            //check request
            HttpRequestHeaders requestHeaders = request.Headers;
            Assert.Equal("apikey-test", requestHeaders.Single(x => x.Key.Equals("x-mock-apikey")).Value.ToArray()[0]);
            Assert.Equal(MockRequest.GetBasicAuthorizationHeader(), requestHeaders.Single(x => x.Key.Equals("Authorization")).Value.ToArray()[0]);
            Assert.Equal(2, requestHeaders.Count());

            mockHttpClient.Verify(x => x.GetAsync(It.Is<Uri>(y => y.AbsoluteUri.Equals(requestUrl))), Times.Once);
            mockHttpClient.VerifyGet(x => x.DefaultRequestHeaders, Times.AtLeast(2));
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

            Mock<IHttpClientDecorator> mockHttpClient = MockRequest.GetMockHttpClientDecorator(HttpMethod.Get, responseHeaders, responseBody, responseStatusCode);

            IRestRequest request = MockRequest.GetRestRequest(requestUrl, mockHttpClient.Object);

            /*
             * 
             * ACT
             * 
             */

            request.Headers.Add("x-mock-apikey", new string[] { "apikey-test" });
            request.UseBasicAuthentication(MockRequest.USERNAME, MockRequest.PASSWORD);
            request.QueryParams.Add("name", "ReadAllTest");

            RestResponse<List<TestModel>> response = request.ReadListAsync<TestModel>().Result;

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

            //check request
            HttpRequestHeaders requestHeaders = request.Headers;
            Assert.Equal("apikey-test", requestHeaders.Single(x => x.Key.Equals("x-mock-apikey")).Value.ToArray()[0]);
            Assert.Equal(MockRequest.GetBasicAuthorizationHeader(), requestHeaders.Single(x => x.Key.Equals("Authorization")).Value.ToArray()[0]);
            Assert.Equal(2, requestHeaders.Count());

            string expectedUri = "http://domain.net/test/v1/test?name=ReadAllTest";
            mockHttpClient.Verify(x => x.GetAsync(It.Is<Uri>(y => y.AbsoluteUri.Equals(expectedUri))), Times.Once);
            mockHttpClient.VerifyGet(x => x.DefaultRequestHeaders, Times.AtLeast(2));
        }

        [Fact]
        public void TestReadOne()
        {
            /*
             * 
             * ARRANGE 
             * 
             */

            string requestUrl = "http://domain.net/test/v1/test/:test_id";

            Dictionary<string, IEnumerable<string>> responseHeaders = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "read-test-1" } }
            };

            TestModel dataExpected = new TestModel() { Id = 1, Name = "ReadAllTest", Status = true };

            string responseBody = JsonConvert.SerializeObject(dataExpected);

            const HttpStatusCode responseStatusCode = HttpStatusCode.OK;

            Mock<IHttpClientDecorator> mockHttpClient = MockRequest.GetMockHttpClientDecorator(HttpMethod.Get, responseHeaders, responseBody, responseStatusCode);

            IRestRequest request = MockRequest.GetRestRequest(requestUrl, mockHttpClient.Object);

            request.Params.Add("test_id", "1");
            request.Headers.Add("x-mock-apikey", new string[] { "apikey-test" });
            request.UseBasicAuthentication(MockRequest.USERNAME, MockRequest.PASSWORD);

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
            Assert.Equal(responseBody, metadata.JsonBody);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);


            //check request
            HttpRequestHeaders requestHeaders = request.Headers;
            Assert.Equal("apikey-test", requestHeaders.Single(x => x.Key.Equals("x-mock-apikey")).Value.ToArray()[0]);
            Assert.Equal(MockRequest.GetBasicAuthorizationHeader(), requestHeaders.Single(x => x.Key.Equals("Authorization")).Value.ToArray()[0]);
            Assert.Equal(2, requestHeaders.Count());

            string expectedUri = "http://domain.net/test/v1/test/1"; 
            mockHttpClient.Verify(x => x.GetAsync(It.Is<Uri>(y => y.AbsoluteUri.Equals(expectedUri))), Times.Once);
            mockHttpClient.VerifyGet(x => x.DefaultRequestHeaders, Times.AtLeast(2));
        }
    }
}
