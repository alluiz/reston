using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NU2Rest
{
    public interface IRestResponseEngine
    {
        Task<RestResponse<TResponseDataModel>> ProcessMessageAsync<TResponseDataModel>(HttpResponseMessage responseMessage, HttpStatusCode expectedStatusCode) where TResponseDataModel : new();
    }

    public class RestResponseEngine : IRestResponseEngine
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
            else
            {
                ProcessResponseError(response);
            }

            return response;
        }

        private void ProcessResponseError<TResponseDataModel>(RestResponse<TResponseDataModel> response) where TResponseDataModel : new()
        {
            HttpStatusCode responseStatusCode = (HttpStatusCode)response.MetaData.StatusCode;

            switch (responseStatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    {
                        throw new RestAuthenticationException("401 - Unauthorized!", response.MetaData);
                    }
                case HttpStatusCode.Forbidden:
                    {
                        throw new RestAuthenticationException("403 - Forbbiden!", response.MetaData);
                    }
                default:
                    {
                        throw new RestException($"{responseStatusCode}- An error has ocurred!", response.MetaData);
                    }
            }
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
                Headers = responseMessage.Headers.ToDictionary(x => x.Key, x => x.Value),
                StatusCode = (int)responseMessage.StatusCode,
                JsonBody = jsonResponseBody
            };

            return metadata;
        }
    }
}