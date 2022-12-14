using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestOn.Auth;
using RestOn.Exception;
using RestOn.Http;
using RestOn.Response;
using RestOn.Utils;

namespace RestOn.Request
{
    /// <summary>
    /// Class for make REST requests
    /// </summary>
    public class RestRequest : IRestRequest
    {
        /// <value>
        /// Default HTTP Port: 80
        /// </value>
        private const int HTTP_PORT_DEFAULT = 80;

        /// <value>
        /// Default HTTP scheme: HTTP
        /// </value>
        private const string HTTP_SCHEME_DEFAULT = "http";

        /// <value>
        /// HTTP Client singleton instance
        /// </value>
        private readonly IHttpClientDecorator _httpClient;

        /// <value>
        /// REST Response engine object for process responses
        /// </value>
        private readonly IRestResponseEngine _responseEngine;

        /// <summary>
        /// The content-type to be used
        /// </summary>
        private MediaTypeHeaderValue contentType;

        /// <value>
        /// Headers that will be sent into the REST request
        /// </value>
        public HttpRequestHeaders Headers
        {
            get => _httpClient.DefaultRequestHeaders;
        }

        /// <value>
        /// Path params that will be replaced in the path
        /// </value>
        public Dictionary<string, string> Params { get; private set; }

        /// <value>
        /// Query params that will be sent into the REST request
        /// </value>
        public Dictionary<string, string> QueryParams { get; private set; }

        /// <value>
        /// The HTTP Port
        /// </value>
        public int Port { get; set; }

        /// <value>
        /// The Host
        /// </value>
        public string Host { get; set; }

        /// <value>
        /// The REST request path
        /// </value>
        public string Path { get; set; }

        /// <value>
        /// HTTP Scheme. Can be HTTPS or HTTP
        /// </value>
        public string Scheme { get; private set; }

        /// <value>
        /// Initialize the properties objects
        /// </value>
        private void InitProperties()
        {
            Params = new Dictionary<string, string>();
            QueryParams = new Dictionary<string, string>();
        }

        /// <summary>
        /// Create a REST request instance
        /// </summary>
        /// <param name="host">The host server. Eg.: "google.com"</param>
        /// <param name="port">The HTTP port</param>
        /// <param name="path">The REST request path</param>
        /// <param name="httpClient">The HTTP Client instance</param>
        /// <param name="responseEngine">The REST response engine instance</param>
        public RestRequest(string host, int port, string path, IHttpClientDecorator httpClient, IRestResponseEngine responseEngine)
        {
            Host = host;
            Path = path;
            Port = port;
            Scheme = HTTP_SCHEME_DEFAULT;

            this._httpClient = httpClient;
            this._responseEngine = responseEngine;

            InitProperties();
        }

        /// <summary>
        /// Create a REST request instance. Uses a default HTTP Port: 80
        /// </summary>
        /// <param name="host">The host server. Eg.: "google.com"</param>
        /// <param name="path">The REST request path</param>
        /// <param name="httpClient">The HTTP Client instance</param>
        /// <param name="responseEngine">The REST response engine instance</param>
        public RestRequest(string host, string path, IHttpClientDecorator httpClient, IRestResponseEngine responseEngine)
        {
            Host = host;
            Path = path;
            Port = HTTP_PORT_DEFAULT;
            Scheme = HTTP_SCHEME_DEFAULT;

            this._httpClient = httpClient;
            this._responseEngine = responseEngine;

            InitProperties();
        }

        /// <summary>
        /// Create a REST request instance
        /// </summary>
        /// <param name="url">The full URL. Must be a valid URL!</param>
        /// <param name="httpClient">The HTTP Client instance</param>
        /// <param name="responseEngine">The REST response engine instance</param>
        public RestRequest(string url, IHttpClientDecorator httpClient, IRestResponseEngine responseEngine)
        {
            Uri uri = new Uri(url);

            Host = uri.Host;
            Port = uri.Port;
            Path = uri.AbsolutePath;
            Scheme = uri.Scheme;

            this._httpClient = httpClient;
            this._responseEngine = responseEngine;

            InitProperties();
        }
        
        /// <summary>
        /// Create a REST request instance
        /// </summary>
        /// <param name="uri">The URI</param>
        /// <param name="httpClient">The HTTP Client instance</param>
        /// <param name="responseEngine">The REST response engine instance</param>
        public RestRequest(Uri uri, IHttpClientDecorator httpClient, IRestResponseEngine responseEngine)
        {
            Host = uri.Host;
            Port = uri.Port;
            Path = uri.AbsolutePath;
            Scheme = uri.Scheme;

            this._httpClient = httpClient;
            this._responseEngine = responseEngine;

            InitProperties();
        }

        /// <summary>
        /// Converts the <paramref name="data"/> in json format
        /// </summary>
        /// <typeparam name="TRequestDataModel">The Request Model Type</typeparam>
        /// <param name="data"><c>TRequestDataModel</c> object</param>
        /// <param name="settings"></param>
        /// <returns>A string in json format</returns>
        private StringContent GetContentBody<TRequestDataModel>(TRequestDataModel data)
        {
            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(data, Formatting.Indented);
            StringContent content = GetContentBody(jsonBody, "application/json");

            return content;
        }

        /// <summary>
        /// /// Converts the <paramref name="data"/> in application/x-www-form-urlencoded
        /// </summary>
        /// <param name="data"><c>Dictionary</c> object</param>
        /// <returns>A string in application/x-www-form-urlencoded format</returns>
        private StringContent GetContentBody(Dictionary<string, string> data)
        {
            return GetContentBody(ProcessParams(data), "application/x-www-form-urlencoded");
        }

        private StringContent GetContentBody(string data, string contentTypeText)
        {
            return new StringContent(data, Encoding.UTF8, contentTypeText);
        }

        public async Task<RestResponse<List<TResponseDataModel>>> GetListAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await SendAsync<List<TResponseDataModel>>(expectedStatusCode, HttpMethod.Get);
        }

        public async Task<RestResponse<TResponseDataModel>> GetAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await SendAsync<TResponseDataModel>(expectedStatusCode, HttpMethod.Get);
        }

        private async Task<RestResponse<TResponseDataModel>> PostAsync<TResponseDataModel>(StringContent content, HttpStatusCode expectedStatusCode)
        {
            return await SendAsync<TResponseDataModel>(content, expectedStatusCode, HttpMethod.Post);
        }

        public async Task<RestResponse<TResponseDataModel>> PostAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created)
        {
            return await PostAsync<TResponseDataModel>(GetContentBody(data), expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> PostAsync<TResponseDataModel>(Dictionary<string, string> data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created)
        {
            return await PostAsync<TResponseDataModel>(GetContentBody(data), expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> PostAsync<TResponseDataModel>(string data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created)
        {
            return await PostAsync<TResponseDataModel>(GetContentBody(data, contentType.MediaType), expectedStatusCode);
        }

        private async Task<RestResponse<TResponseDataModel>> PutAsync<TResponseDataModel>(StringContent content, HttpStatusCode expectedStatusCode)
        {
            return await SendAsync<TResponseDataModel>(content, expectedStatusCode, HttpMethod.Put);
        }

        public async Task<RestResponse<TResponseDataModel>> PutAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await PutAsync<TResponseDataModel>(GetContentBody(data), expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> PutAsync<TResponseDataModel>(Dictionary<string, string> data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await PutAsync<TResponseDataModel>(GetContentBody(data), expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> PutAsync<TResponseDataModel>(string data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await PutAsync<TResponseDataModel>(GetContentBody(data, contentType.MediaType), expectedStatusCode);
        }

        private async Task<RestResponse<TResponseDataModel>> PatchAsync<TResponseDataModel>(StringContent content, HttpStatusCode expectedStatusCode)
        {
            return await SendAsync<TResponseDataModel>(content, expectedStatusCode, HttpMethod.Patch);
        }

        public async Task<RestResponse<TResponseDataModel>> PatchAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await PatchAsync<TResponseDataModel>(GetContentBody(data), expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> PatchAsync<TResponseDataModel>(Dictionary<string, string> data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await PatchAsync<TResponseDataModel>(GetContentBody(data), expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> PatchAsync<TResponseDataModel>(string data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await PatchAsync<TResponseDataModel>(GetContentBody(data, contentType.MediaType), expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> DeleteAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            return await SendAsync<TResponseDataModel>(expectedStatusCode, HttpMethod.Delete);
        }

        private async Task<RestResponse<TResponseDataModel>> SendAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode, HttpMethod method)
        {
            return await SendAsync<TResponseDataModel>(null, expectedStatusCode, method);
        }

        public async Task<RestResponse<TResponseDataModel>> SendAsync<TResponseDataModel>(StringContent content, HttpStatusCode expectedStatusCode, HttpMethod method)
        {
            try
            {
                HttpResponseMessage responseMessage = await SendAsync(content, method);

                RestResponse<TResponseDataModel> response = await _responseEngine
                    .ProcessMessageAsync<TResponseDataModel>(responseMessage, expectedStatusCode);

                return response;
            }
            catch (RestException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private async Task<HttpResponseMessage> SendAsync(StringContent content, HttpMethod method)
        {
            Uri requestUri = GetRequestUri();

            switch (method.Method)
            {
                case "GET":
                    return await _httpClient.GetAsync(requestUri);
                case "POST":
                    return await _httpClient.PostAsync(requestUri, content);
                default:
                    {
                        var message = new HttpRequestMessage(method, requestUri);
                        message.Content = content;
                        return await _httpClient.SendAsync(message);
                    }
            }
        }

        public IRestRequest UseHttps()
        {
            //Define the scheme to HTTPS
            Scheme = "https";

            //Set the port number ONLY if it's default value 80. Otherwise, maintain the choosed port.
            if (Port == 80)
                Port = 443;

            return this;
        }

        public IRestRequest UseContentType(MediaTypeHeaderValue mediaTypeHeaderValue)
        {
            this.contentType = mediaTypeHeaderValue;

            return this;
        }

        private Uri GetRequestUri()
        {
            ProcessPathParams();

            UriBuilder builder = new UriBuilder(Scheme, Host, Port, Path);

            builder.Query = ProcessParams(QueryParams);

            Uri requestUri = builder.Uri;

            return requestUri;
        }

        private void ProcessPathParams()
        {
            Regex regex = new Regex(@":\w+");

            MatchCollection collection = regex.Matches(Path);

            if (collection.Count > 0)
            {
                foreach (Match match in collection)
                {
                    string pathParam = match.Value;
                    string param = pathParam.Replace(":", string.Empty);

                    if (Params.Keys.Contains(param))
                        Path = Path.Replace(pathParam, Params[param]);
                    else
                        throw new ArgumentNullException(param, "The path parameter was not found. Please, add this to 'Params' property before send the request.");
                }
            }
        }

        private string ProcessParams(Dictionary<string, string> parameters)
        {
            string query = string.Empty;

            if (parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    string key = param.Key;
                    string value = param.Value;

                    if (!string.IsNullOrEmpty(query))
                        query = $"{query}&";

                    query = $"{query}{key}={value}";
                }
            }

            return query;
        }

        public IRestRequest UseBearerAuthentication(OAuth2Token token)
        {
            UseAuthentication(RestAuthentication.BEARER, token.AccessToken);

            return this;
        }

        private void UseAuthentication(RestAuthentication authenticationType, string credentials)
        {
            switch (authenticationType)
            {
                case RestAuthentication.BEARER:
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", credentials);
                        break;
                    }
                case RestAuthentication.BASIC:
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Authentication type is invalid.", authenticationType.ToString());
                    }
            }

        }

        public IRestRequest UseAuthentication(string authenticationType, string credentials)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationType, credentials);

            return this;
        }

        public IRestRequest UseBasicAuthentication(string username, string password)
        {
            string credentials = $"{username}:{password}";

            string credentials64 = Encoding.UTF8.EncodeBase64(credentials);

            UseAuthentication(RestAuthentication.BASIC, credentials64);
            
            return this;
        }
    }
}
