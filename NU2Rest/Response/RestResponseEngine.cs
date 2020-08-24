using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NU2Rest
{
    public class RestResponseEngine
    {
        public async Task<RestResponse<TResponseDataModel>> ProcessMessageAsync<TResponseDataModel>(HttpResponseMessage responseMessage, HttpStatusCode expectedStatusCode) where TResponseDataModel : new()
        {
            RestResponse<TResponseDataModel> response = new RestResponse<TResponseDataModel>();
            
            response.MetaData = await ProcessMetadataAsync(responseMessage);

            //Process response data only if the status code was the expected. Otherwise, process only metadata.
            if (responseMessage.StatusCode.Equals(expectedStatusCode))
            {
                response.Data = ProcessData<TResponseDataModel>(response.MetaData.JsonBody);
            }

            return response;
        }

        private TResponseDataModel ProcessData<TResponseDataModel>(string jsonResponseBody)
        {
            TResponseDataModel responseData = JsonConvert.DeserializeObject<TResponseDataModel>(jsonResponseBody);

            return responseData;
        }

        private async Task<RestResponseMetadata> ProcessMetadataAsync(HttpResponseMessage responseMessage)
        {
            string jsonResponseBody = await responseMessage.Content.ReadAsStringAsync();

            RestResponseMetadata metadata = new RestResponseMetadata()
            {
                Headers = responseMessage.Headers.ToDictionary(x => x),
                StatusCode = (int)responseMessage.StatusCode,
                JsonBody = jsonResponseBody
            };

            return metadata;
        }
    }
}