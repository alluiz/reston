using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NU2Rest
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
        private const string HTTTP_SCHEME_DEFAULT = "http";

        /// <value>
        /// Default JSON Serializer settings. Null values will be ignored.
        /// </value>
        private JsonSerializerSettings defaultSettings;

        /// <value>
        /// HTTP Client singleton instance
        /// </value>
        private readonly HttpClient httpClient;

        /// <value>
        /// REST Response engine object for process responses
        /// </value>
        private readonly IRestResponseEngine responseEngine;

        /// <value>
        /// Headers that will be sent into the REST request
        /// </value>
        public Dictionary<string, IEnumerable<string>> Headers { get; private set; }

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
            Headers = new Dictionary<string, IEnumerable<string>>();
            Params = new Dictionary<string, string>();
            QueryParams = new Dictionary<string, string>();
            Scheme = HTTTP_SCHEME_DEFAULT;
            defaultSettings = InitJsonDefaultSettings();
        }

        /// <summary>
        /// Initialize JSON Default settings
        /// </summary>
        /// <returns>The JSON Serializer default settings</returns>
        private JsonSerializerSettings InitJsonDefaultSettings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.NullValueHandling = NullValueHandling.Ignore;

            return settings;
        }

        /// <summary>
        /// Create a REST request instance
        /// </summary>
        /// <param name="host">The host server. Eg.: "google.com"</param>
        /// <param name="port">The HTTP port</param>
        /// <param name="path">The REST request path</param>
        /// <param name="httpClient">The HTTP Client instance</param>
        /// <param name="responseEngine">The REST response engine instance</param>
        public RestRequest(string host, int port, string path, HttpClient httpClient, IRestResponseEngine responseEngine)
        {
            Host = host;
            Path = path;
            Port = port;

            this.httpClient = httpClient;
            this.responseEngine = responseEngine;

            InitProperties();
        }

        /// <summary>
        /// Create a REST request instance. Uses a default HTTP Port: 80
        /// </summary>
        /// <param name="host">The host server. Eg.: "google.com"</param>
        /// <param name="path">The REST request path</param>
        /// <param name="httpClient">The HTTP Client instance</param>
        /// <param name="responseEngine">The REST response engine instance</param>
        public RestRequest(string host, string path, HttpClient httpClient, IRestResponseEngine responseEngine)
        {
            Host = host;
            Path = path;
            Port = HTTP_PORT_DEFAULT;

            this.httpClient = httpClient;
            this.responseEngine = responseEngine;

            InitProperties();
        }

        /// <summary>
        /// Create a REST request instance
        /// </summary>
        /// <param name="url">The full URL. Must be a valid URL!</param>
        /// <param name="httpClient">The HTTP Client instance</param>
        /// <param name="responseEngine">The REST response engine instance</param>
        public RestRequest(string url, HttpClient httpClient, IRestResponseEngine responseEngine)
        {
            Uri uri = new Uri(url);

            Host = uri.Host;
            Port = uri.Port;
            Path = uri.AbsolutePath;
            Scheme = uri.Scheme;

            this.httpClient = httpClient;
            this.responseEngine = responseEngine;

            InitProperties();
        }

        /// <summary>
        /// Converts the <paramref name="data"/> in json format
        /// </summary>
        /// <typeparam name="TRequestDataModel">The Request Model Type</typeparam>
        /// <param name="data"><c>TRequestDataModel</c> object</param>
        /// <param name="settings"></param>
        /// <returns>A string in json format</returns>
        private StringContent GetContentBody<TRequestDataModel>(TRequestDataModel data, JsonSerializerSettings settings)
        {
            settings = CheckJsonSerializerSettings(settings);
            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(data, Formatting.Indented, settings);
            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            return content;
        }

        private async Task<RestResponse<TResponseDataModel>> DoRequestAsync<TResponseDataModel>(Func<Uri, Task<HttpResponseMessage>> requestAsync, HttpStatusCode expectedStatusCode) where TResponseDataModel : new()
        {
            try
            {
                Uri requestUri = GetRequestUri();

                HttpResponseMessage responseMessage = await requestAsync(requestUri);

                RestResponse<TResponseDataModel> response = await responseEngine
                    .ProcessMessageAsync<TResponseDataModel>(
                        responseMessage: responseMessage,
                        expectedStatusCode: expectedStatusCode);

                return response;
            }
            catch (RestException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<RestResponse<TResponseDataModel>> ReadAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK) where TResponseDataModel : new()
        {
            return await DoRequestAsync<TResponseDataModel>(
                async (requestUri) =>
                {
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);

                    return responseMessage;
                }, expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> CreateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created, JsonSerializerSettings settings = null) where TResponseDataModel : new()
        {
            return await DoRequestAsync<TResponseDataModel>(
                async (requestUri) =>
                {
                    StringContent content = GetContentBody<TRequestDataModel>(data, settings);
                    HttpResponseMessage responseMessage = await httpClient.PostAsync(requestUri, content);

                    return responseMessage;
                }, expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> UpdateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) where TResponseDataModel : new()
        {
            return await DoRequestAsync<TResponseDataModel>(
                async (requestUri) =>
                {
                    StringContent content = GetContentBody<TRequestDataModel>(data, settings);
                    HttpResponseMessage responseMessage = await httpClient.PutAsync(requestUri, content);

                    return responseMessage;
                }, expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> UpdatePartialAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) where TResponseDataModel : new()
        {
            return await DoRequestAsync<TResponseDataModel>(
                async (requestUri) =>
                {
                    StringContent content = GetContentBody<TRequestDataModel>(data, settings);
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Patch, requestUri);
                    HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

                    return responseMessage;
                }, expectedStatusCode);
        }

        public async Task<RestResponse<TResponseDataModel>> DestroyAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent) where TResponseDataModel : new()
        {
            return await DoRequestAsync<TResponseDataModel>(
                 async (requestUri) =>
                 {
                     HttpResponseMessage responseMessage = await httpClient.DeleteAsync(requestUri);

                     return responseMessage;
                 }, expectedStatusCode);
        }

        private JsonSerializerSettings CheckJsonSerializerSettings(JsonSerializerSettings settings)
        {
            //if null, set default camelCase
            settings = settings ?? defaultSettings;

            return settings;
        }

        public void UseHttps()
        {
            //Define the scheme to HTTPS
            Scheme = "https";

            //Set the port number ONLY if it's default value 80. Otherwise, maintain the choosed port.
            if (Port == 80)
                Port = 443;
        }

        private Uri GetRequestUri()
        {
            ProcessParams();

            UriBuilder builder = new UriBuilder(Scheme, Host, Port, Path);

            builder.Query = ProcessQueryParams();

            Uri requestUri = builder.Uri;

            return requestUri;
        }

        private void ProcessParams()
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

        private string ProcessQueryParams()
        {
            string query = string.Empty;

            if (QueryParams.Count > 0)
            {
                foreach (KeyValuePair<string, string> param in QueryParams)
                {
                    string key = param.Key;
                    string value = param.Value;

                    if (!string.IsNullOrEmpty(query))
                        query = query + '&';

                    query = query + key + '=' + value;
                }
            }

            return query;
        }

        public void UseBearerAuthentication(string access_token)
        {
            UseAuthentication(RestAuthentication.BEARER, access_token);
        }

        private void UseAuthentication(RestAuthentication authenticationType, string credentials)
        {
            string authorization = string.Empty;

            switch (authenticationType)
            {
                case RestAuthentication.BEARER:
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", credentials);
                        break;
                    }
                case RestAuthentication.BASIC:
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Authentication type is invalid.", authenticationType.ToString());
                    }
            }

        }

        public void UseBasicAuthentication(string username, string password)
        {
            string credentials = $"{username}:{password}";

            string credentials64 = Encoding.UTF8.EncodeBase64(credentials);

            UseAuthentication(RestAuthentication.BASIC, credentials64);
        }
    }
}
