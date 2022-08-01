using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using RestOn.Service;
using System.Net;

namespace RestOn.Auth
{
    public class IdentityProviderService : IIdentityProviderService
    {
        private readonly IRestRequest requestToken;
        private readonly IRestRequest introspectToken;
        public OAuth2Credentials Credentials { private get; set; }
        public IdentityProviderService(OAuth2Endpoints endpoints, IRestService restService)
        {
            this.requestToken = restService.CreateRequest(endpoints.TokenUri);
            this.introspectToken = restService.CreateRequest(endpoints.TokenIntrospectionUri);
        }
        
        public IdentityProviderService(OAuth2Endpoints endpoints, OAuth2Credentials credentials, IRestService restService): 
            this(endpoints, restService)
        {
            this.Credentials = credentials;
        }

        public async Task<bool> ValidateToken(OAuth2Token token)
        {
            var data = new Dictionary<string, string>();
            data.Add("access_token", token.Access_Token);
            
            var response = await requestToken.PostAsync<OAuth2TokenInstrospection>(data, HttpStatusCode.OK);

            return response.Data.Active;
        }
        public async Task<OAuth2Token> GetToken(OpenIdConnectCredentials oidcCredentials)
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
            data.Add("refresh_token", token.Refresh_Token);
            
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
            var response = await requestToken.PostAsync<OAuth2Token>(data, HttpStatusCode.OK);

            return response.Data;
        }
    }
}