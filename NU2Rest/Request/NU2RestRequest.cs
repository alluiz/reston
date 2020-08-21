using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NU2Rest
{
    public class NU2RestRequest : INU2RestRequest
    {
        private const string SCHEME_DEFAULT = "http";

        public string Body { get; set; }
        public string Path { get; set; }

        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Params { get; set; }
        public Dictionary<string, string> QueryParams { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        private string scheme = SCHEME_DEFAULT;
        private readonly HttpClient httpClient;


        public NU2RestRequest(string url, HttpClient httpClient)
        {
            Uri uri = new Uri(url);

            Host = uri.Host;
            Port = uri.Port;
            Path = uri.AbsolutePath;
            scheme = uri.Scheme;

            this.httpClient = httpClient;

            Params = new Dictionary<string, string>();
            QueryParams = new Dictionary<string, string>();
        }

        public NU2RestRequest(string host, string path, HttpClient httpClient)
        {
            Host = host;
            Path = path;
            Port = 80;
            this.httpClient = httpClient;
            Params = new Dictionary<string, string>();
            QueryParams = new Dictionary<string, string>();
        }

        public NU2RestRequest(string host, int port, string path, HttpClient httpClient)
        {
            Host = host;
            Path = path;
            Port = port;
            this.httpClient = httpClient;
            Params = new Dictionary<string, string>();
            QueryParams = new Dictionary<string, string>();
        }

        public async Task<NU2RestResponse<TResponseDataModel>> ReadAsync<TResponseDataModel>() where TResponseDataModel : new()
        {
            Uri requestUri = GetRequestUri();

            NU2RestResponse<TResponseDataModel> response = default(NU2RestResponse<TResponseDataModel>);

            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);

                response = await processResponseMessageAsync<TResponseDataModel>(responseMessage);
            }
            catch (System.Exception)
            {
                throw;
            }

            return response;
        }

        private async Task<NU2RestResponse<TResponseDataModel>> processResponseMessageAsync<TResponseDataModel>(HttpResponseMessage responseMessage) where TResponseDataModel : new()
        {
            string jsonResponseBody = await responseMessage.Content.ReadAsStringAsync();

            NU2RestResponse<TResponseDataModel> response = new NU2RestResponse<TResponseDataModel>();

            response.Data = ProcessData<TResponseDataModel>(jsonResponseBody);
            response.MetaData = ProcessMetadata(responseMessage);

            return response;
        }

        private TResponseDataModel ProcessData<TResponseDataModel>(string jsonResponseBody)
        {
            TResponseDataModel responseData = JsonConvert.DeserializeObject<TResponseDataModel>(jsonResponseBody);

            return responseData;
        }

        private NU2RestResponseMetadata ProcessMetadata(HttpResponseMessage responseMessage)
        {
            NU2RestResponseMetadata metadata = new NU2RestResponseMetadata()
            {
                Headers = responseMessage.Headers.ToDictionary(x => x),
                StatusCode = (int)responseMessage.StatusCode
            };

            return metadata;
        }

        public async Task<NU2RestResponse<TResponseDataModel>> CreateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, JsonSerializerSettings settings = null) where TResponseDataModel : new()
        {
            settings = CheckJsonSerializerSettings(settings);

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(data, Formatting.Indented, settings);

            Uri requestUri = GetRequestUri();

            NU2RestResponse<TResponseDataModel> response = default(NU2RestResponse<TResponseDataModel>);

            try
            {
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await httpClient.PostAsync(requestUri, content);

                response = await processResponseMessageAsync<TResponseDataModel>(responseMessage);
            }
            catch (System.Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<NU2RestResponse<TResponseDataModel>> UpdateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, JsonSerializerSettings settings = null) where TResponseDataModel : new()
        {
            settings = CheckJsonSerializerSettings(settings);

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(data, Formatting.Indented, settings);

            Uri requestUri = GetRequestUri();

            NU2RestResponse<TResponseDataModel> response = default(NU2RestResponse<TResponseDataModel>);

            try
            {
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await httpClient.PutAsync(requestUri, content);

                response = await processResponseMessageAsync<TResponseDataModel>(responseMessage);
            }
            catch (System.Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<NU2RestResponse<TResponseDataModel>> UpdatePartialAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, JsonSerializerSettings settings = null) where TResponseDataModel : new()
        {
            settings = CheckJsonSerializerSettings(settings);

            string jsonRequestBody = Newtonsoft.Json.JsonConvert.SerializeObject(data, Formatting.Indented, settings);

            Uri requestUri = GetRequestUri();

            NU2RestResponse<TResponseDataModel> response = default(NU2RestResponse<TResponseDataModel>);

            try
            {
                StringContent content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Patch, requestUri);
                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

                string jsonResponseBody = await responseMessage.Content.ReadAsStringAsync();
                response = await processResponseMessageAsync<TResponseDataModel>(responseMessage);

            }
            catch (System.Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<NU2RestResponse<TResponseDataModel>> DestroyAsync<TResponseDataModel>() where TResponseDataModel : new()
        {
            Uri requestUri = GetRequestUri();

            NU2RestResponse<TResponseDataModel> response = default(NU2RestResponse<TResponseDataModel>);

            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);
                response = await processResponseMessageAsync<TResponseDataModel>(responseMessage);
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
            scheme = "https";

            if (Port == 80)
                Port = 443;
        }

        private Uri GetRequestUri()
        {
            ProcessParams();

            UriBuilder builder = new UriBuilder(scheme, Host, Port, Path);

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
