using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestOn.Http;
using System;
using System.Net.Http;
using RestOn.Request;

namespace RestOn.Service
{
    public class RestService : BaseService, IRestService
    {
        /// <summary>
        /// New instance Rest Service. Specify the scheme that will be used 
        /// </summary>
        /// <param name="scheme">The scheme will be used in the requests. HTTP (default) or HTTPS</param>
        public RestService(RestScheme scheme): base(scheme)
        {
        }

        /// <summary>
        /// New instance Rest Service. Uses HTTP Scheme by default
        /// </summary>
        public RestService() : base(RestScheme.HTTP)
        {
        }

        /// <summary>
        /// Create RestRequest instance
        /// </summary>
        /// <param name="host">The host server. Eg.: domain.com</param>
        /// <param name="path">The path param. Eg.: /test</param>
        /// <returns>A new RestRequest instance</returns>
        public IRestRequest CreateRequest(string host, string path)
        {
            IRestRequest request = new RestRequest(host, path, HttpClient, ResponseEngine);
            SetSchemeDefault(request);

            return request;
        }

        /// <summary>
        /// Create RestRequest instance
        /// </summary>
        /// <param name="url">The full URL. Eg.: https://domain.com/test</param>
        /// <returns>A new RestRequest instance</returns>
        public IRestRequest CreateRequest(string url)
        {
            IRestRequest request = new RestRequest(url, HttpClient, ResponseEngine);
            SetSchemeDefault(request);

            return request;
        }

        public IRestRequest CreateRequest(Uri uri)
        {
            IRestRequest request = new RestRequest(uri, HttpClient, ResponseEngine);
            SetSchemeDefault(request);

            return request;
        }

        /// <summary>
        /// Create RestRequest instance
        /// </summary>
        /// <param name="host">The host server. Eg.: domain.com</param>
        /// <param name="port">The server port. Eg.: 443</param>
        /// <param name="path">The path param. Eg.: /test</param>
        /// <returns>A new RestRequest instance</returns>
        public IRestRequest CreateRequest(string host, int port, string path)
        {
            IRestRequest request = new RestRequest(host, port, path, HttpClient, ResponseEngine);
            SetSchemeDefault(request);

            return request;
        }
    }
}