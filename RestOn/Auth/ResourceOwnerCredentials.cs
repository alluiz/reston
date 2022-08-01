using System;
using System.Collections.Generic;
using System.Text;

namespace RestOn.Auth
{
    public class ResourceOwnerCredentials
    {
        public ResourceOwnerCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}
