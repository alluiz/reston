using System.Threading.Tasks;

namespace RestOn.Auth
{
    public interface IResouceOwnerProvider: IRefreshProvider
    {
        /// <summary>
        /// Ask for a new RESOURCE OWNER token
        /// </summary>
        /// <param name="resourceOwnerCredentials">The resource owner credentials</param>
        /// <returns>A new token</returns>
        Task<OAuth2Token> GetToken(ResourceOwnerCredentials resourceOwnerCredentials);
    }
}