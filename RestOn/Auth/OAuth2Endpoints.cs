using System;

namespace RestOn.Auth
{
    public class OAuth2Endpoints
    {
        public OAuth2Endpoints(string tokenUri, string tokenIntrospectionUri)
        {
            this.TokenUri = new Uri(tokenUri);
            this.TokenIntrospectionUri = new Uri(tokenIntrospectionUri);
        }

        public Uri TokenUri { get; }
        public Uri TokenIntrospectionUri { get; }
    }
}