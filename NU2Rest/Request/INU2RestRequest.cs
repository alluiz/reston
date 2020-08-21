using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NU2Rest
{
    public interface INU2RestRequest
    {
        string Body { get; set; }
        string Path { get; set; }
        Dictionary<string, string> Headers { get; set; }
        Dictionary<string, string> Params { get; set; }
        Dictionary<string, string> QueryParams { get; set; }
        string Host { get; set; }
        int Port { get; set; }

        Task<NU2RestResponse<TResponseDataModel>> CreateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, JsonSerializerSettings settings = null) where TResponseDataModel : new();
        Task<NU2RestResponse<TResponseDataModel>> DestroyAsync<TResponseDataModel>() where TResponseDataModel : new();
        Task<NU2RestResponse<TResponseDataModel>> ReadAsync<TResponseDataModel>() where TResponseDataModel : new();
        Task<NU2RestResponse<TResponseDataModel>> UpdateAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, JsonSerializerSettings settings = null) where TResponseDataModel : new();
        Task<NU2RestResponse<TResponseDataModel>> UpdatePartialAsync<TRequestDataModel, TResponseDataModel>(TRequestDataModel data, JsonSerializerSettings settings = null) where TResponseDataModel : new();
        void UseHttps();
    }
}