using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NU2Rest
{
    public class NU2RestResponseEngine
    {
        public async Task<NU2RestResponse<TResponseDataModel>> ProcessMessageAsync<TResponseDataModel>(HttpResponseMessage responseMessage) where TResponseDataModel : new()
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
    }
}