using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace NU2Rest.OAuth2
{
    public class TokenService : BaseService, ITokenService
    {
        private OAuth2Token token;
        private IRestRequest requestToken;
        private JsonSerializerSettings jsonSerializerSettings;

        public TokenService(TokenServiceProvider tokenServiceProvider) : base(RestScheme.HTTPS)
        {
            TokenServiceProvider = tokenServiceProvider;
        }

        public TokenServiceProvider TokenServiceProvider { get; set; }

        private IRestRequest GetRequestToken()
        {
            if (requestToken == null)
            {
                requestToken = new RestRequest(TokenServiceProvider.TokenUrl, HttpClient, ResponseEngine);
                SetSchemeDefault(requestToken);
            }

            return requestToken;
        }

        public OAuth2Token GetResourceOwnerToken(ResourceOwnerCredentials resourceOwnerCredentials)
        {
            bool validToken = ValidateToken();

            if (!validToken)
                token = CallTokenServiceProvider(resourceOwnerCredentials);

            return token;
        }

        private bool ValidateToken()
        {
            return false;
        }

        private OAuth2Token CallTokenServiceProvider(ResourceOwnerCredentials resourceOwnerCredentials)
        {
            IRestRequest requestToken = GetRequestToken();
            JsonSerializerSettings jsonSerializerSettings = GetJsonSerializerSettings();

            RestResponse<OAuth2Token> response = requestToken.CreateAsync<ResourceOwnerCredentials, OAuth2Token>(resourceOwnerCredentials, System.Net.HttpStatusCode.OK, jsonSerializerSettings).Result;

            Console.WriteLine(response.MetaData.ToJsonString());

            OAuth2Token token = response.Data;

            return token;
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            if (jsonSerializerSettings == null)
            {
                jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                };
            }

            return jsonSerializerSettings;
        }
    }
}
