using System.Net.Http;

namespace NU2Rest
{
    public interface IRestService
    {
        RestScheme Scheme { get; set; }

        IRestRequest CreateRequest(string host, string path);
        IRestRequest CreateRequest(string url);
        IRestRequest CreateRequest(string host, int port, string path);
        void setSchemeDefault(IRestRequest request);
    }

    public class RestService : IRestService
    {
        public RestService(RestScheme scheme)
        {
            Scheme = scheme;
        }

        public RestService()
        {
            Scheme = RestScheme.HTTP;
        }

        public RestScheme Scheme { get; set; }

        private IHttpClientDecorator httpClient;
        private IHttpClientDecorator HttpClient
        {
            get
            {
                if (httpClient == null)
                    httpClient = new HttpClientDecorator();

                return httpClient;
            }
        }

        private RestResponseEngine responseEngine;
        private RestResponseEngine ResponseEngine
        {
            get
            {
                if (responseEngine == null)
                    responseEngine = new RestResponseEngine();

                return responseEngine;
            }
        }

        public IRestRequest CreateRequest(string host, string path)
        {
            IRestRequest request = new RestRequest(host, path, HttpClient, ResponseEngine);
            setSchemeDefault(request);

            return request;
        }

        public IRestRequest CreateRequest(string url)
        {
            IRestRequest request = new RestRequest(url, HttpClient, ResponseEngine);
            setSchemeDefault(request);

            return request;
        }

        public IRestRequest CreateRequest(string host, int port, string path)
        {
            IRestRequest request = new RestRequest(host, port, path, HttpClient, ResponseEngine);
            setSchemeDefault(request);

            return request;
        }

        public void setSchemeDefault(IRestRequest request)
        {
            switch (Scheme)
            {
                case RestScheme.HTTP:
                    {
                        break;
                    }
                case RestScheme.HTTPS:
                    {
                        request.UseHttps();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}