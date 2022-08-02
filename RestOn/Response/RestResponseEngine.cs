using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestOn.Exception;

namespace RestOn.Response
{
    public class RestResponseEngine : IRestResponseEngine
    {
        public async Task<RestResponse<TResponseDataModel>> ProcessMessageAsync<TResponseDataModel>(
            HttpResponseMessage responseMessage, 
            HttpStatusCode expectedStatusCode) 
        {
            RestResponse<TResponseDataModel> response = new RestResponse<TResponseDataModel>();

            response.Metadata = await ProcessMetadataAsync(responseMessage);

            //Process response data only if the status code was the expected. Otherwise, process only metadata.
            if (responseMessage.StatusCode.Equals(expectedStatusCode))
            {
                if (typeof(TResponseDataModel) == typeof(string))
                    response.Data = (TResponseDataModel) Convert.ChangeType(response.Metadata.Body, typeof(TResponseDataModel));
                else if (typeof(TResponseDataModel) == typeof(int))
                    response.Data = (TResponseDataModel) Convert.ChangeType(response.Metadata.Body, typeof(TResponseDataModel));
                else
                    response.Data = ProcessData<TResponseDataModel>(response.Metadata.Body);
            }
            else
            {
                ProcessResponseError(response);
            }

            return response;
        }

        private void ProcessResponseError<TResponseDataModel>(RestResponse<TResponseDataModel> response) 
        {
            HttpStatusCode responseStatusCode = (HttpStatusCode)response.Metadata.StatusCode;

            switch (responseStatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    {
                        throw new RestAuthenticationException("401 - Unauthorized!", response.Metadata);
                    }
                case HttpStatusCode.Forbidden:
                    {
                        throw new RestAuthenticationException("403 - Forbbiden!", response.Metadata);
                    }
                default:
                    {
                        throw new RestException($"{responseStatusCode}- An error has ocurred!", response.Metadata);
                    }
            }
        }

        private TResponseDataModel ProcessData<TResponseDataModel>(string responseBody)
        {
            TResponseDataModel responseData = JsonConvert.DeserializeObject<TResponseDataModel>(responseBody);

            return responseData;
        }

        private async Task<RestResponseMetadata> ProcessMetadataAsync(HttpResponseMessage responseMessage)
        {
            string jsonResponseBody = await responseMessage.Content.ReadAsStringAsync();

            RestResponseMetadata metadata = new RestResponseMetadata()
            {
                Headers = responseMessage.Headers.ToDictionary(x => x.Key, x => x.Value),
                StatusCode = (int)responseMessage.StatusCode,
                Body = jsonResponseBody
            };

            return metadata;
        }
    }
}