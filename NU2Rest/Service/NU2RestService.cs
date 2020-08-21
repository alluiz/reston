using System.Net.Http;

namespace NU2Rest
{
    public class NU2RestService
    {
        public NU2RestService(NU2RestScheme scheme)
        {
            Scheme = scheme;
        }

        public NU2RestService()
        {
            Scheme = NU2RestScheme.HTTP;
        }

        public NU2RestScheme Scheme { get; set; }

        private HttpClient httpClient;
        private HttpClient HttpClient
        {
            get
            {
                if (httpClient == null)
                    httpClient = new HttpClient();

                return httpClient;
            }
        }

        private NU2RestResponseEngine responseEngine;
        private NU2RestResponseEngine ResponseEngine 
        {
             get
             {
                 if (responseEngine == null)
                    responseEngine = new NU2RestResponseEngine();
                
                return responseEngine;
             } 
        }

        public INU2RestRequest CreateRequest(string host, string path)
        {
            INU2RestRequest request = new NU2RestRequest(host, path, HttpClient, ResponseEngine);
            setSchemeDefault(request);

            return request;
        }

        public INU2RestRequest CreateRequest(string url)
        {
            INU2RestRequest request = new NU2RestRequest(url, HttpClient, ResponseEngine);
            setSchemeDefault(request);

            return request;
        }

        public INU2RestRequest CreateRequest(string host, int port, string path)
        {
            INU2RestRequest request = new NU2RestRequest(host, port, path, HttpClient, ResponseEngine);
            setSchemeDefault(request);

            return request;
        }

        public void setSchemeDefault(INU2RestRequest request)
        {
            switch (Scheme)
            {
                case NU2RestScheme.HTTP:
                    {
                        break;
                    }
                case NU2RestScheme.HTTPS:
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