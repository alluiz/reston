using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using RestOn;
using RestOn.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace RestOn.Tests
{
    public class SimpleUrlTest
    {
        private readonly HttpClientDecorator httpClient = new HttpClientDecorator();

        [Fact]
        public void SimpleUrlWithHttpRequestTest()
        {
            string url = "http://domain.com";

            IRestRequest request = new RestRequest(url, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(80, request.Port);
            Assert.Equal("/", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void SimpleUrlWithHttpAndSubDomainRequestTest()
        {
            string url = "http://subdomain.domain.com";

            IRestRequest request = new RestRequest(url, httpClient, new RestResponseEngine());

            Assert.Equal("subdomain.domain.com", request.Host);
            Assert.Equal(80, request.Port);
            Assert.Equal("/", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void SimpleUrlWithHttpsAndSubDomainRequestTest()
        {
            string url = "https://subdomain.domain.com";

            IRestRequest request = new RestRequest(url, httpClient, new RestResponseEngine());

            Assert.Equal("subdomain.domain.com", request.Host);
            Assert.Equal(443, request.Port);
            Assert.Equal("/", request.Path);
            Assert.Equal("https", request.Scheme);

        }

        [Fact]
        public void SimpleUrlWithHttpsRequestTest()
        {
            string url = "https://domain.com";

            IRestRequest request = new RestRequest(url, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(443, request.Port);
            Assert.Equal("/", request.Path);
            Assert.Equal("https", request.Scheme);

        }

        [Fact]
        public void SimpleUrlWithHttpAndPortRequestTest()
        {
            string url = "http://domain.com:8075";

            IRestRequest request = new RestRequest(url, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(8075, request.Port);
            Assert.Equal("/", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void SimpleUrlWithHttpsAndPortRequestTest()
        {
            string url = "https://domain.com:8075";

            IRestRequest request = new RestRequest(url, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(8075, request.Port);
            Assert.Equal("/", request.Path);
            Assert.Equal("https", request.Scheme);

        }

        [Fact]
        public void SimpleUrlWithHttpAndPathRequestTest()
        {
            string url = "http://domain.com/path";

            IRestRequest request = new RestRequest(url, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(80, request.Port);
            Assert.Equal("/path", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void SimpleUrlWithHttpsAndPathRequestTest()
        {
            string url = "https://domain.com/path";

            IRestRequest request = new RestRequest(url, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(443, request.Port);
            Assert.Equal("/path", request.Path);
            Assert.Equal("https", request.Scheme);

        }

        [Fact]
        public void SimpleUrlWithHttpAndPortAndPathRequestTest()
        {
            string url = "http://domain.com:8075/path";

            IRestRequest request = new RestRequest(url, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(8075, request.Port);
            Assert.Equal("/path", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void SimpleUrlWithHttpsAndPortAndPathRequestTest()
        {
            string url = "https://domain.com:8075/path";

            IRestRequest request = new RestRequest(url, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(8075, request.Port);
            Assert.Equal("/path", request.Path);
            Assert.Equal("https", request.Scheme);

        }
    }
}
