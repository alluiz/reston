using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestOn.Auth;

namespace RestOn
{
    public interface IRestRequest
    {
        HttpRequestHeaders Headers { get; }
        Dictionary<string, string> Params { get; }
        Dictionary<string, string> QueryParams { get; }
        int Port { get; set; }
        string Host { get; set; }
        string Path { get; set; }
        string Scheme { get; }
        StringContent GetContentBody<TRequestDataModel>(TRequestDataModel data, JsonSerializerSettings settings);
        StringContent GetContentBody(Dictionary<string, string> data);
        StringContent GetContentBody(string data, string contentType);
        Task<RestResponse<TResponseDataModel>> DeleteAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent);
        Task<RestResponse<TResponseDataModel>> GetAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<List<TResponseDataModel>>> GetListAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<TResponseDataModel>> PatchAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null);
        Task<RestResponse<TResponseDataModel>> PatchAsync<TResponseDataModel>(Dictionary<string, string> data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<TResponseDataModel>> PatchAsync<TResponseDataModel>(string data, string contentType, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null);
        Task<RestResponse<TResponseDataModel>> PostAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created, JsonSerializerSettings settings = null);
        Task<RestResponse<TResponseDataModel>> PostAsync<TResponseDataModel>(Dictionary<string, string> data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created);
        Task<RestResponse<TResponseDataModel>> PostAsync<TResponseDataModel>(string data, string contentType, HttpStatusCode expectedStatusCode = HttpStatusCode.Created);
        Task<RestResponse<TResponseDataModel>> PutAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null);
        Task<RestResponse<TResponseDataModel>> PutAsync<TResponseDataModel>(Dictionary<string, string> data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null);
        Task<RestResponse<TResponseDataModel>> PutAsync<TResponseDataModel>(string data, string contentType, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null);
        Task<RestResponse<TResponseDataModel>> SendAsync<TResponseDataModel>(StringContent content, HttpStatusCode expectedStatusCode, HttpMethod method);
        void UseAuthentication(string authenticationType, string credentials);
        void UseBasicAuthentication(string username, string password);
        void UseBearerAuthentication(OAuth2Token token);
        void UseHttps();
    }
}
