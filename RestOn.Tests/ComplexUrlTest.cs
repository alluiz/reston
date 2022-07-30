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
    public class ComplexUrlTest
    {
        private readonly HttpClientDecorator httpClient = new HttpClientDecorator();

        [Fact]
        public void ComplexUrlWithHttpRequestTest()
        {
            string host = "domain.com";
            string path = "/";

            IRestRequest request = new RestRequest(host, path, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(80, request.Port);
            Assert.Equal("/", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void ComplexUrlWithHttpAndNoPathRequestTest()
        {
            string host = "domain.com";
            string path = "";

            IRestRequest request = new RestRequest(host, path, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(80, request.Port);
            Assert.Equal("", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void ComplexUrlWithHttpAndSubDomainRequestTest()
        {
            string host = "subdomain.domain.com";
            string path = "";

            IRestRequest request = new RestRequest(host, path, httpClient, new RestResponseEngine());

            Assert.Equal("subdomain.domain.com", request.Host);
            Assert.Equal(80, request.Port);
            Assert.Equal("", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void ComplexUrlWithHttpsAndSubDomainRequestTest()
        {
            string host = "subdomain.domain.com";
            string path = "";

            IRestRequest request = new RestRequest(host, path, httpClient, new RestResponseEngine());

            request.UseHttps();

            Assert.Equal("subdomain.domain.com", request.Host);
            Assert.Equal(443, request.Port);
            Assert.Equal("", request.Path);
            Assert.Equal("https", request.Scheme);

        }

        [Fact]
        public void ComplexUrlWithHttpsRequestTest()
        {
            string host = "domain.com";
            string path = "";

            IRestRequest request = new RestRequest(host, path, httpClient, new RestResponseEngine());

            request.UseHttps();

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(443, request.Port);
            Assert.Equal("", request.Path);
            Assert.Equal("https", request.Scheme);

        }

        [Fact]
        public void ComplexUrlWithHttpAndPortRequestTest()
        {
            string host = "domain.com";
            int port = 8075;
            string path = "";

            IRestRequest request = new RestRequest(host, port, path, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(8075, request.Port);
            Assert.Equal("", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void ComplexUrlWithHttpsAndPortRequestTest()
        {
            string host = "domain.com";
            int port = 8075;
            string path = "";

            IRestRequest request = new RestRequest(host, port, path, httpClient, new RestResponseEngine());

            request.UseHttps();

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(8075, request.Port);
            Assert.Equal("", request.Path);
            Assert.Equal("https", request.Scheme);

        }

        [Fact]
        public void ComplexUrlWithHttpAndPathRequestTest()
        {
            string host = "domain.com";
            string path = "/path";

            IRestRequest request = new RestRequest(host, path, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(80, request.Port);
            Assert.Equal("/path", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void ComplexUrlWithHttpsAndPathRequestTest()
        {
            string host = "domain.com";
            string path = "/path";

            IRestRequest request = new RestRequest(host, path, httpClient, new RestResponseEngine());

            request.UseHttps();

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(443, request.Port);
            Assert.Equal("/path", request.Path);
            Assert.Equal("https", request.Scheme);

        }

        [Fact]
        public void ComplexUrlWithHttpAndPortAndPathRequestTest()
        {
            string host = "domain.com"; int port = 8075; string path = "/path";

            IRestRequest request = new RestRequest(host, port, path, httpClient, new RestResponseEngine());

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(8075, request.Port);
            Assert.Equal("/path", request.Path);
            Assert.Equal("http", request.Scheme);

        }

        [Fact]
        public void ComplexUrlWithHttpsAndPortAndPathRequestTest()
        {
            string host = "domain.com"; int port = 8075; string path = "/path";

            IRestRequest request = new RestRequest(host, port, path, httpClient, new RestResponseEngine());

            request.UseHttps();

            Assert.Equal("domain.com", request.Host);
            Assert.Equal(8075, request.Port);
            Assert.Equal("/path", request.Path);
            Assert.Equal("https", request.Scheme);

        }
    }
}
