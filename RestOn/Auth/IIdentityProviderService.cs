using System.Threading.Tasks;

namespace RestOn.Auth
{
    public interface IIdentityProviderService
    {
        OAuth2Credentials Credentials { set; }
        Task<OAuth2Token> GetToken(OpenIdConnectCredentials oidcCredentials);
        Task<OAuth2Token> GetToken(OAuth2Token token);
        Task<OAuth2Token> GetToken(ResourceOwnerCredentials resourceOwnerCredentials);
        Task<OAuth2Token> GetToken();
        Task<bool> ValidateToken(OAuth2Token token);
    }
}