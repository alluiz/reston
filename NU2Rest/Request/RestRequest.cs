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
    public class RestRequest : IRestRequest
    {
        private const int PORT_DEFAULT = 80;
        //Use this if no scheme was specified
        private const string SCHEME_DEFAULT = "http";
        private readonly HttpClient httpClient;
        private readonly RestResponseEngine responseEngine;

        public Dictionary<string, string> Headers { get; private set; }
        public Dictionary<string, string> Params { get; private set; }
        public Dictionary<string, string> QueryParams { get; private set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string Scheme { get; set; }

        private void InitProperties()
        {
            Headers = new Dictionary<string, string>();
            Params = new Dictionary<string, string>();
            QueryParams = new Dictionary<string, string>();
            Scheme = SCHEME_DEFAULT;
        }
        public RestRequest(string host, int port, string path, HttpClient httpClient, RestResponseEngine responseEngine)
        {
            Host = host;
            Path = path;
            Port = port;

            this.httpClient = httpClient;
            this.responseEngine = responseEngine;

            InitProperties();
        }
        public RestRequest(string host, string path, HttpClient httpClient, RestResponseEngine responseEngine)
        {
            Host = host;
            Path = path;
            Port = PORT_DEFAULT;

            this.httpClient = httpClient;
            this.responseEngine = responseEngine;

            InitProperties();
        }
        public RestRequest(string url, HttpClient httpClient, RestResponseEngine responseEngine)
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

        public async Task<RestResponse<TResponseDataModel>> ReadAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) where TResponseDataModel : new()
        {
            RestResponse<TResponseDataModel> response = null;

            try
            {

                settings = CheckJsonSerializerSettings(settings);

                Uri requestUri = GetRequestUri();

                HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);

                response = await responseEngine.ProcessMessageAsync<TResponseDataModel>(responseMessage, expectedStatusCode: expectedStatusCode);
            }
            catch (System.Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<RestResponse<TResponseDataModel>> CreateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created, JsonSerializerSettings settings = null) where TResponseDataModel : new()
        {
            settings = CheckJsonSerializerSettings(settings);

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(data, Formatting.Indented, settings);

            Uri requestUri = GetRequestUri();

            RestResponse<TResponseDataModel> response = default(RestResponse<TResponseDataModel>);

            try
            {
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await httpClient.PostAsync(requestUri, content);

                response = await responseEngine.ProcessMessageAsync<TResponseDataModel>(responseMessage, expectedStatusCode: expectedStatusCode);
            }
            catch (System.Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<RestResponse<TResponseDataModel>> UpdateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) where TResponseDataModel : new()
        {
            settings = CheckJsonSerializerSettings(settings);

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(data, Formatting.Indented, settings);

            Uri requestUri = GetRequestUri();

            RestResponse<TResponseDataModel> response = default(RestResponse<TResponseDataModel>);

            try
            {
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await httpClient.PutAsync(requestUri, content);

                response = await responseEngine.ProcessMessageAsync<TResponseDataModel>(responseMessage, expectedStatusCode: expectedStatusCode);
            }
            catch (System.Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<RestResponse<TResponseDataModel>> UpdatePartialAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) where TResponseDataModel : new()
        {
            settings = CheckJsonSerializerSettings(settings);

            string jsonRequestBody = Newtonsoft.Json.JsonConvert.SerializeObject(data, Formatting.Indented, settings);

            Uri requestUri = GetRequestUri();

            RestResponse<TResponseDataModel> response = default(RestResponse<TResponseDataModel>);

            try
            {
                StringContent content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Patch, requestUri);
                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

                string jsonResponseBody = await responseMessage.Content.ReadAsStringAsync();
                response = await responseEngine.ProcessMessageAsync<TResponseDataModel>(responseMessage, expectedStatusCode: expectedStatusCode);

            }
            catch (System.Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<RestResponse<TResponseDataModel>> DestroyAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent) where TResponseDataModel : new()
        {
            Uri requestUri = GetRequestUri();

            RestResponse<TResponseDataModel> response = default(RestResponse<TResponseDataModel>);

            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);
                response = await responseEngine.ProcessMessageAsync<TResponseDataModel>(responseMessage, expectedStatusCode: expectedStatusCode);
            }
            catch (System.Exception)
            {
                throw;
            }

            return response;
        }

        private static JsonSerializerSettings CheckJsonSerializerSettings(JsonSerializerSettings settings)
        {
            //if null, set default camelCase
            if (settings == null)
            {
                settings = new JsonSerializerSettings();
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                settings.NullValueHandling = NullValueHandling.Ignore;
            }

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
    }
}
