using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NU2Rest.OAuth2;

namespace NU2Rest
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

        Task<RestResponse<TResponseDataModel>> CreateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created, JsonSerializerSettings settings = null) where TResponseDataModel : new();
        Task<RestResponse<TResponseDataModel>> DestroyAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent) where TResponseDataModel : new();
        Task<RestResponse<TResponseDataModel>> ReadAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK) where TResponseDataModel : new();
        Task<RestResponse<TResponseDataModel>> UpdateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) where TResponseDataModel : new();
        void UseBearerAuthentication(OAuth2Token token);
        void UseBasicAuthentication(string username, string password);
        Task<RestResponse<TResponseDataModel>> UpdatePartialAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) where TResponseDataModel : new();
        void UseHttps();
        Task<RestResponse<List<TResponseDataModel>>> ReadListAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK) where TResponseDataModel : new();
    }
}