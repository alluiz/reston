using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NU2Rest
{
    public interface IRestRequest
    {
        string Path { get; set; }
        Dictionary<string, string> Headers { get; }
        Dictionary<string, string> Params { get; }
        Dictionary<string, string> QueryParams { get; }
        string Host { get; set; }
        int Port { get; set; }

        Task<RestResponse<TResponseDataModel>> CreateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.Created, JsonSerializerSettings settings = null) where TResponseDataModel : new();
        Task<RestResponse<TResponseDataModel>> DestroyAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent) where TResponseDataModel : new();
        Task<RestResponse<TResponseDataModel>> ReadAsync<TResponseDataModel>(HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) where TResponseDataModel : new();
        Task<RestResponse<TResponseDataModel>> UpdateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) where TResponseDataModel : new();
        Task<RestResponse<TResponseDataModel>> UpdatePartialAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, JsonSerializerSettings settings = null) where TResponseDataModel : new();
        void UseHttps();
    }
}