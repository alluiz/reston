using System;
using Xunit;
using System.Net.Http;
using Moq;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using RestOn.Http;

namespace RestOn.Tests
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

            string requestUri = "http://domain.net/test/v1/test";
            string requestUriExpected = "http://domain.net/test/v1/test";

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

            Mock<IHttpClientDecorator> mockHttpClient = MockRequest.GetMockHttpClientDecorator(new Uri(requestUriExpected), HttpMethod.Get, responseHeaders, responseBody, responseStatusCode);

            IRestRequest request = MockRequest.GetRestRequest(requestUri, mockHttpClient.Object);

            request.Headers.Add("x-mock-apikey", new string[]{ "apikey-test" });
            request.UseBasicAuthentication(MockRequest.USERNAME, MockRequest.PASSWORD);

            /*
             * 
             * ACT
             * 
             */

            RestResponse<List<TestModel>> response = request.GetListAsync<TestModel>().Result;

            List<TestModel> data = response.Data;
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
            Assert.Equal((int)HttpStatusCode.OK, metadata.StatusCode);
            Assert.Equal(responseBody, metadata.Body);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);

            //check request
            HttpRequestHeaders requestHeaders = request.Headers;
            Assert.Equal("apikey-test", requestHeaders.Single(x => x.Key.Equals("x-mock-apikey")).Value.ToArray()[0]);
            Assert.Equal(MockRequest.GetBasicAuthorizationHeader(), requestHeaders.Single(x => x.Key.Equals("Authorization")).Value.ToArray()[0]);
            Assert.Equal(2, requestHeaders.Count());

            mockHttpClient.Verify(x => x.GetAsync(It.Is<Uri>(y => y.AbsoluteUri.Equals(requestUriExpected))), Times.Once);
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

            string requestUri = "http://domain.net/test/v1/test";
            string requestUriExpected = "http://domain.net/test/v1/test?name=ReadAllTest";

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

            Mock<IHttpClientDecorator> mockHttpClient = MockRequest.GetMockHttpClientDecorator(new Uri(requestUriExpected), HttpMethod.Get, responseHeaders, responseBody, responseStatusCode);

            IRestRequest request = MockRequest.GetRestRequest(requestUri, mockHttpClient.Object);

            /*
             * 
             * ACT
             * 
             */

            request.Headers.Add("x-mock-apikey", new string[] { "apikey-test" });
            request.UseBasicAuthentication(MockRequest.USERNAME, MockRequest.PASSWORD);
            request.QueryParams.Add("name", "ReadAllTest");

            RestResponse<List<TestModel>> response = request.GetListAsync<TestModel>().Result;

            List<TestModel> data = response.Data;
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

            //Check querry params
            Assert.Single(request.QueryParams);
            Assert.Equal("ReadAllTest", request.QueryParams["name"]);

            //Check model data
            Assert.Equal(dataExpected, data, new TestModelComparer());

            //Check metadata
            Assert.Equal((int)HttpStatusCode.OK, metadata.StatusCode);
            Assert.Equal(responseBody, metadata.Body);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-2", metadata.Headers["x-correlationId"].ToArray()[0]);

            //check request
            HttpRequestHeaders requestHeaders = request.Headers;
            Assert.Equal("apikey-test", requestHeaders.Single(x => x.Key.Equals("x-mock-apikey")).Value.ToArray()[0]);
            Assert.Equal(MockRequest.GetBasicAuthorizationHeader(), requestHeaders.Single(x => x.Key.Equals("Authorization")).Value.ToArray()[0]);
            Assert.Equal(2, requestHeaders.Count());

            mockHttpClient.Verify(x => x.GetAsync(It.Is<Uri>(y => y.AbsoluteUri.Equals(requestUriExpected))), Times.Once);
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

            string requestUri = "http://domain.net/test/v1/test/:test_id";
            string requestUriExpected = "http://domain.net/test/v1/test/1";

            Dictionary<string, IEnumerable<string>> responseHeaders = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "read-test-1" } }
            };

            TestModel dataExpected = new TestModel() { Id = 1, Name = "ReadAllTest", Status = true };

            string responseBody = JsonConvert.SerializeObject(dataExpected);

            const HttpStatusCode responseStatusCode = HttpStatusCode.OK;

            Mock<IHttpClientDecorator> mockHttpClient = MockRequest.GetMockHttpClientDecorator(new Uri(requestUriExpected), HttpMethod.Get, responseHeaders, responseBody, responseStatusCode);

            IRestRequest request = MockRequest.GetRestRequest(requestUri, mockHttpClient.Object);

            request.Params.Add("test_id", "1");
            request.Headers.Add("x-mock-apikey", new string[] { "apikey-test" });
            request.UseBasicAuthentication(MockRequest.USERNAME, MockRequest.PASSWORD);

            /*
             * 
             * ACT
             * 
             */

            RestResponse<TestModel> response = request.GetAsync<TestModel>().Result;

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

            //Check path params
            Assert.Single(request.Params);
            Assert.Equal("1", request.Params["test_id"]);

            //Check model data
            Assert.Equal(dataExpected, data, new TestModelComparer());

            //Check metadata
            Assert.Equal((int)HttpStatusCode.OK, metadata.StatusCode);
            Assert.Equal(responseBody, metadata.Body);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);


            //check request
            HttpRequestHeaders requestHeaders = request.Headers;
            Assert.Equal("apikey-test", requestHeaders.Single(x => x.Key.Equals("x-mock-apikey")).Value.ToArray()[0]);
            Assert.Equal(MockRequest.GetBasicAuthorizationHeader(), requestHeaders.Single(x => x.Key.Equals("Authorization")).Value.ToArray()[0]);
            Assert.Equal(2, requestHeaders.Count());

            mockHttpClient.Verify(x => x.GetAsync(It.Is<Uri>(y => y.AbsoluteUri.Equals(requestUriExpected))), Times.Once);
            mockHttpClient.VerifyGet(x => x.DefaultRequestHeaders, Times.AtLeast(2));
        }

        [Fact]
        public void TestReadOneStringResponse()
        {
            /*
             * 
             * ARRANGE 
             * 
             */

            string requestUri = "http://domain.net/test/v1/test/:test_id";
            string requestUriExpected = "http://domain.net/test/v1/test/1";

            Dictionary<string, IEnumerable<string>> responseHeaders = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "read-test-1" } }
            };

            string dataExpected = "my-test";

            string responseBody = dataExpected;

            const HttpStatusCode responseStatusCode = HttpStatusCode.OK;

            Mock<IHttpClientDecorator> mockHttpClient = MockRequest.GetMockHttpClientDecorator(new Uri(requestUriExpected), HttpMethod.Get, responseHeaders, responseBody, responseStatusCode);

            IRestRequest request = MockRequest.GetRestRequest(requestUri, mockHttpClient.Object);

            request.Params.Add("test_id", "1");
            request.Headers.Add("x-mock-apikey", new string[] { "apikey-test" });
            request.UseBasicAuthentication(MockRequest.USERNAME, MockRequest.PASSWORD);

            /*
             * 
             * ACT
             * 
             */

            RestResponse<string> response = request.GetAsync<string>().Result;

            string data = response.Data;
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

            //Check path params
            Assert.Single(request.Params);
            Assert.Equal("1", request.Params["test_id"]);

            //Check model data
            Assert.Equal(dataExpected, data);

            //Check metadata
            Assert.Equal((int)HttpStatusCode.OK, metadata.StatusCode);
            Assert.Equal(responseBody, metadata.Body);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);


            //check request
            HttpRequestHeaders requestHeaders = request.Headers;
            Assert.Equal("apikey-test", requestHeaders.Single(x => x.Key.Equals("x-mock-apikey")).Value.ToArray()[0]);
            Assert.Equal(MockRequest.GetBasicAuthorizationHeader(), requestHeaders.Single(x => x.Key.Equals("Authorization")).Value.ToArray()[0]);
            Assert.Equal(2, requestHeaders.Count());

            mockHttpClient.Verify(x => x.GetAsync(It.Is<Uri>(y => y.AbsoluteUri.Equals(requestUriExpected))), Times.Once);
            mockHttpClient.VerifyGet(x => x.DefaultRequestHeaders, Times.AtLeast(2));
        }

        [Fact]
        public void TestReadOneIntegerResponse()
        {
            /*
             * 
             * ARRANGE 
             * 
             */

            string requestUri = "http://domain.net/test/v1/test/:test_id";
            string requestUriExpected = "http://domain.net/test/v1/test/1";

            Dictionary<string, IEnumerable<string>> responseHeaders = new Dictionary<string, IEnumerable<string>>
            {
                { "x-correlationId", new string[] { "read-test-1" } }
            };

            int dataExpected = 100;

            string responseBody = dataExpected.ToString();

            const HttpStatusCode responseStatusCode = HttpStatusCode.OK;

            Mock<IHttpClientDecorator> mockHttpClient = MockRequest.GetMockHttpClientDecorator(new Uri(requestUriExpected), HttpMethod.Get, responseHeaders, responseBody, responseStatusCode);

            IRestRequest request = MockRequest.GetRestRequest(requestUri, mockHttpClient.Object);

            request.Params.Add("test_id", "1");
            request.Headers.Add("x-mock-apikey", new string[] { "apikey-test" });
            request.UseBasicAuthentication(MockRequest.USERNAME, MockRequest.PASSWORD);

            /*
             * 
             * ACT
             * 
             */

            RestResponse<int> response = request.GetAsync<int>().Result;

            int data = response.Data;
            RestResponseMetadata metadata = response.Metadata;

            /*
             * 
             * ASSERT
             * 
             */

            //Check if not null
            Assert.NotNull(metadata);
            Assert.NotNull(request.Headers);
            Assert.NotNull(request.Params);
            Assert.NotNull(request.QueryParams);

            //Check path params
            Assert.Single(request.Params);
            Assert.Equal("1", request.Params["test_id"]);

            //Check model data
            Assert.Equal(dataExpected, data);

            //Check metadata
            Assert.Equal((int)HttpStatusCode.OK, metadata.StatusCode);
            Assert.Equal(responseBody, metadata.Body);
            Assert.NotNull(metadata.Headers);
            Assert.Single(metadata.Headers);
            Assert.Single(metadata.Headers["x-correlationId"]);
            Assert.Equal("read-test-1", metadata.Headers["x-correlationId"].ToArray()[0]);


            //check request
            HttpRequestHeaders requestHeaders = request.Headers;
            Assert.Equal("apikey-test", requestHeaders.Single(x => x.Key.Equals("x-mock-apikey")).Value.ToArray()[0]);
            Assert.Equal(MockRequest.GetBasicAuthorizationHeader(), requestHeaders.Single(x => x.Key.Equals("Authorization")).Value.ToArray()[0]);
            Assert.Equal(2, requestHeaders.Count());

            mockHttpClient.Verify(x => x.GetAsync(It.Is<Uri>(y => y.AbsoluteUri.Equals(requestUriExpected))), Times.Once);
            mockHttpClient.VerifyGet(x => x.DefaultRequestHeaders, Times.AtLeast(2));
        }
    }
}
