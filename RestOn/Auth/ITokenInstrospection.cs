using System;
using System.Threading.Tasks;

namespace RestOn.Auth
{
    public interface ITokenInstrospection
    {
        /// <summary>
        /// The token instrospection uri
        /// </summary>
        public Uri TokenIntrospectionUri { get; set; }
        
        /// <summary>
        /// Indicates if TOKEN is valid
        /// </summary>
        /// <param name="token">The token to check</param>
        /// <returns>The result of validation</returns>
        Task<bool> ValidateToken(OAuth2Token token);
    }
}