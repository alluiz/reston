using System.Threading.Tasks;

namespace RestOn.Auth
{
    public interface IOpenIdConnectProvider: IRefreshProvider
    {
        /// <summary>
        /// Ask for a new OIDC Token
        /// </summary>
        /// <param name="oidcCredentials">The OIDC credentials</param>
        /// <returns>A new token</returns>
        Task<OAuth2Token> ExchangeForToken(OpenIdConnectCredentials oidcCredentials);
    }
}