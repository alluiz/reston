using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestOn.Response
{
    public interface IRestResponseEngine
    {
        Task<RestResponse<TResponseDataModel>> ProcessMessageAsync<TResponseDataModel>(HttpResponseMessage responseMessage, HttpStatusCode expectedStatusCode);
    }
}