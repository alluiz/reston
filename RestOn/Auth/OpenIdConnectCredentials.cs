using System;
using System.Collections.Generic;
using System.Text;

namespace RestOn.Auth
{
    public class OpenIdConnectCredentials
    {
        public OpenIdConnectCredentials( string code, string redirect_uri)
        {
            Code = code;
            RedirectUri = redirect_uri;
        }

        public string Code { get; set; }
        public string RedirectUri { get; set; }
    }
}
