using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace NU2Rest.OAuth2
{
    public class TokenService : BaseService, ITokenService
    {
        private OAuth2Token localToken;

        public TokenService(string url) : base(RestScheme.HTTPS)
        {
            Url = url;
        }

        public string Url { get; set; }

        public IRestRequest CreateRequest()
        {
            IRestRequest request = new RestRequest(Url, HttpClient, ResponseEngine);
            SetSchemeDefault(request);

            return request;
        }

        public OAuth2Token GetResourceOwnerToken(ResourceOwnerCredentials resourceOwnerCredentials)
        {
            bool validToken = ValidateLocalToken();

            if (!validToken)
                localToken = CallTokenServiceProvider(resourceOwnerCredentials);

            return localToken;
        }

        private bool ValidateLocalToken()
        {
            return false;
        }

        private OAuth2Token CallTokenServiceProvider(ResourceOwnerCredentials resourceOwnerCredentials)
        {
            IRestRequest request = CreateRequest(Url);

            Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            RestResponse<OAuth2Token> response = request.CreateAsync<ResourceOwnerCredentials, OAuth2Token>(resourceOwnerCredentials, System.Net.HttpStatusCode.OK, jsonSerializerSettings).Result;

            OAuth2Token token = response.Data;

            return token;
        }

        /// <summary>
        /// Create RestRequest instance
        /// </summary>
        /// <param name="url">The full URL. Eg.: https://domain.com/test</param>
        /// <returns>A new RestRequest instance</returns>
        public IRestRequest CreateRequest(string url)
        {
            IRestRequest request = new RestRequest(url, HttpClient, ResponseEngine);
            SetSchemeDefault(request);

            return request;
        }
    }
}
