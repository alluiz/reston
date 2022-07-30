using System;
using System.Collections.Generic;
using System.Text;

namespace RestOn.Auth
{
    public class OAuth2Token
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public string Token_Type { get; set; }
        public int Expires_In { get; set; }
    }
}
