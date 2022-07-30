using RestOn.Auth;

namespace RestOn.Service
{
    public interface ITokenService
    {
        TokenServiceProvider TokenServiceProvider { get; set; }
        OAuth2Token GetResourceOwnerToken(ResourceOwnerCredentials resourceOwnerCredentials);
    }
}