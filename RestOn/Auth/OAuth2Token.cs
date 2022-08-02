using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RestOn.Auth
{
    public class OAuth2Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        public override string ToString()
        {
            return $"AccessToken: [{AccessToken}]; ExpiresIn: [{ExpiresIn}]";
        }
    }
}
