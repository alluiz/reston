﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NU2Rest.OAuth2
{
    public class ResourceOwnerCredentials: OAuth2Credentials
    {
        public ResourceOwnerCredentials(string username, string password): base(grant_type: "password")
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
