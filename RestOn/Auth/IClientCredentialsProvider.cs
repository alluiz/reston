using System.Threading.Tasks;

namespace RestOn.Auth
{
    public interface IClientCredentialsProvider
    {
        /// <summary>
        /// Ask for a new CLIENT CREDENTIALS token
        /// </summary>
        /// <returns>A new token</returns>
        Task<OAuth2Token> GetToken();
    }
}