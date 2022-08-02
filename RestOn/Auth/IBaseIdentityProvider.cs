using System;
using System.Threading.Tasks;

namespace RestOn.Auth
{
    public interface IBaseIdentityProvider
    {
        /// <summary>
        /// The endpoint to ask for a new token
        /// </summary>
        Uri TokenUri { get; set; }
    }
}