using System.Collections.Generic;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestOn.Service;

namespace RestOn.Auth
{
    public class IdentityService : IIdentityProvider
    {
        private readonly IRestService apiService;

        public Uri TokenIntrospectionUri { get; set; }
        public Uri TokenUri { get; set; }
        public OAuth2Credentials Credentials { get; set; }
        
        /// <summary>
        /// Create a new IdentityService instance
        /// </summary>
        /// <param name="endpoints">The OAuth2 endpoints</param>
        /// <param name="apiService">The apiService for connect to IdP</param>
        public IdentityService(OAuth2Endpoints endpoints, IRestService apiService)
        {
            this.TokenUri = endpoints.TokenUri;
            this.TokenIntrospectionUri = endpoints.TokenIntrospectionUri;
            this.apiService = apiService;
        }
        
        /// <summary>
        /// Create a new IdentityService instance
        /// </summary>
        /// <param name="endpoints">The OAuth2 endpoints</param>
        /// <param name="credentials">The OAuth2 client credentials</param>
        /// <param name="apiService">The apiService for connect to IdP</param>
        public IdentityService(OAuth2Endpoints endpoints, OAuth2Credentials credentials, IRestService apiService): 
            this(endpoints, apiService)
        {
            this.Credentials = credentials;
        }

        public async Task<bool> ValidateToken(OAuth2Token token)
        {
            var data = new Dictionary<string, string>();
            data.Add("access_token", token.AccessToken);
            
            var response = await apiService
                .CreateRequest(TokenIntrospectionUri)
                .PostAsync<OAuth2TokenInstrospection>(data, HttpStatusCode.OK);

            return response.Data.Active;
        }
        public async Task<OAuth2Token> ExchangeForToken(OpenIdConnectCredentials oidcCredentials)
        {
            var data = new Dictionary<string, string>();
            data.Add("grant_type", "authorization_code");
            data.Add("code", oidcCredentials.Code);
            data.Add("redirect_uri", oidcCredentials.RedirectUri);
            
            return await GetToken(data);
        }
        public async Task<OAuth2Token> GetToken(OAuth2Token token)
        {
            var data = new Dictionary<string, string>();
            data.Add("grant_type", "refresh_token");
            data.Add("refresh_token", token.RefreshToken);
            
            return await GetToken(data);
        }
        public async Task<OAuth2Token> GetToken(ResourceOwnerCredentials resourceOwnerCredentials)
        {
            var data = new Dictionary<string, string>();
            data.Add("grant_type", "password");
            data.Add("username", resourceOwnerCredentials.Username);
            data.Add("password", resourceOwnerCredentials.Password);
            
            return await GetToken(data);
        }
        public async Task<OAuth2Token> GetToken()
        {
            var data = new Dictionary<string, string>();
            data.Add("grant_type", "client_credentials");

            return await GetToken(data);
        }
        private async Task<OAuth2Token> GetToken(Dictionary<string, string> data)
        {
            
            data.Add("client_id", Credentials.ClientId);
            data.Add("client_secret", Credentials.ClientSecret);
            var response = await apiService
                .CreateRequest(TokenUri)
                .PostAsync<OAuth2Token>(data, HttpStatusCode.OK);

            return response.Data;
        }
    }
}