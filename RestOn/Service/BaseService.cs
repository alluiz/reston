using RestOn.Http;

namespace RestOn.Service
{
    public abstract class BaseService
    {
        private IHttpClientDecorator httpClient;

        protected IHttpClientDecorator HttpClient
        {
            get
            {
                if (httpClient == null)
                    httpClient = new HttpClientDecorator();

                return httpClient;
            }
        }

        private RestResponseEngine responseEngine;


        /// <summary>
        /// The scheme will be used in the requests. HTTP (default) or HTTPS
        /// </summary>
        public RestScheme Scheme { get; set; }

        protected BaseService(RestScheme scheme)
        {
            this.Scheme = scheme;
        }

        protected RestResponseEngine ResponseEngine
        {
            get
            {
                if (responseEngine == null)
                    responseEngine = new RestResponseEngine();

                return responseEngine;
            }
        }

        protected void SetSchemeDefault(IRestRequest request)
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