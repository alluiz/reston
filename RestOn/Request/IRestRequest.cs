using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestOn.Auth;
using RestOn.Response;

namespace RestOn.Request
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
        Task<RestResponse<TResponseDataModel>> DeleteAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent);
        Task<RestResponse<TResponseDataModel>> GetAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<List<TResponseDataModel>>> GetListAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<TResponseDataModel>> PatchAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<TResponseDataModel>> PatchAsync<TResponseDataModel>(Dictionary<string, string> data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<TResponseDataModel>> PatchAsync<TResponseDataModel>(string data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<TResponseDataModel>> PostAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created);
        Task<RestResponse<TResponseDataModel>> PostAsync<TResponseDataModel>(Dictionary<string, string> data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created);
        Task<RestResponse<TResponseDataModel>> PostAsync<TResponseDataModel>(string data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created);
        Task<RestResponse<TResponseDataModel>> PutAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<TResponseDataModel>> PutAsync<TResponseDataModel>(Dictionary<string, string> data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<TResponseDataModel>> PutAsync<TResponseDataModel>(string data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK);
        Task<RestResponse<TResponseDataModel>> SendAsync<TResponseDataModel>(StringContent content, HttpStatusCode expectedStatusCode, HttpMethod method);
        IRestRequest UseAuthentication(string authenticationType, string credentials);
        IRestRequest UseBasicAuthentication(string username, string password);
        IRestRequest UseBearerAuthentication(OAuth2Token token);
        IRestRequest UseHttps();
        IRestRequest UseContentType(MediaTypeHeaderValue mediaTypeHeaderValue);
    }
}
