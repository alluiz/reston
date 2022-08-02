using System.Threading.Tasks;

namespace RestOn.Auth
{
    public interface IRefreshProvider: IBaseIdentityProvider
    {
        /// <summary>
        /// Ask for a new TOKEN using the old token
        /// </summary>
        /// <param name="token">The old token</param>
        /// <returns>A new token</returns>
        Task<OAuth2Token> GetToken(OAuth2Token token);
    }
}