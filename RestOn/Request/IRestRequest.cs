using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestOn.Auth;

namespace RestOn
{
    public interface IRestRequest
    {
        string Path { get; set; }
        HttpRequestHeaders Headers { get; }
        Dictionary<string, string> Params { get; }
        Dictionary<string, string> QueryParams { get; }
        int Port { get; set; }
        string Host { get; set; }
        string Scheme { get; }
        Task<RestResponse<TResponseDataModel>> PostAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created, JsonSerializerSettings settings = null) ;
        Task<RestResponse<TResponseDataModel>> PostAsync<TResponseDataModel>(Dictionary<string, string> data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created) ;
        Task<RestResponse<TResponseDataModel>> DeleteAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent) ;
        Task<RestResponse<TResponseDataModel>> GetAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK) ;
        Task<RestResponse<List<TResponseDataModel>>> GetListAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK) ;
        Task<RestResponse<TResponseDataModel>> PutAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) ;
        Task<RestResponse<TResponseDataModel>> PatchAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) ;
        void UseBearerAuthentication(OAuth2Token token);
        void UseBasicAuthentication(string username, string password);
        void UseHttps();
    }
}