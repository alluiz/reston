using Moq;
using RestOn;
using RestOn.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace RestOn.Tests
{
    public static class MockRequest
    {
        public const string PASSWORD = "test";
        public const string USERNAME = "mock";

        public static string GetBasicAuthorizationHeader()
        {
            return $"Basic {Encoding.UTF8.EncodeBase64($"{USERNAME}:{PASSWORD}")}";
        }

        public static Mock<IHttpClientDecorator> GetMockHttpClientDecorator(
            Uri requestUri,
            HttpMethod method,
            Dictionary<string, IEnumerable<string>> responseHeaders,
            string responseBody,
            HttpStatusCode responseStatusCode)
        {
            HttpClient httpClient = GetHttpClient(responseHeaders, responseBody, responseStatusCode);

            Mock<IHttpClientDecorator> mock = new Mock<IHttpClientDecorator>();

            mock
                .Setup(x => x.DefaultRequestHeaders)
                .Returns(httpClient.DefaultRequestHeaders)
                .Verifiable();

            switch (method.Method)
            {
                case "GET":
                    {
                        mock
                .Setup(x => x.GetAsync(It.Is<Uri>(u => u.AbsoluteUri.Equals(requestUri.AbsoluteUri))))
                .ReturnsAsync(httpClient.GetAsync(requestUri).Result);
                        break;
                    }
                case "POST":
                    {
                        mock
                .Setup(x => x.PostAsync(It.Is<Uri>(u => u.AbsoluteUri.Equals(requestUri.AbsoluteUri)), It.IsAny<HttpContent>()))
                .ReturnsAsync(httpClient.PostAsync(requestUri, It.IsAny<HttpContent>()).Result);
                        break;
                    }
                default:
                    break;
            }

            return mock;
        }

        public static IRestRequest GetRestRequest(string url, IHttpClientDecorator httpClient)
        {
            RestResponseEngine responseEngine = new RestResponseEngine();

            IRestRequest request = new RestRequest(url, httpClient, responseEngine);

            return request;
        }

        private static HttpClient GetHttpClient(Dictionary<string, IEnumerable<string>> responseHeaders, string responseBody, HttpStatusCode responseStatusCode)
        {
            HttpMessageHandler handler = new MockHttpMessageHandler(responseBody, responseStatusCode, responseHeaders);

            HttpClient httpClient = new HttpClient(handler);

            return httpClient;
        }
    }
}
