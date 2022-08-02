using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestOn
{
    public interface IRestResponseEngine
    {
        Task<RestResponse<TResponseDataModel>> ProcessMessageAsync<TResponseDataModel>(HttpResponseMessage responseMessage, HttpStatusCode expectedStatusCode);
    }
}