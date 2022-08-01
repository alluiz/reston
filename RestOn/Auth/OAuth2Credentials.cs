﻿namespace RestOn.Auth
{
    public class OAuth2Credentials
    {
        public OAuth2Credentials(string clientId, string clientSecret)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}