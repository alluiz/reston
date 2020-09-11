namespace NU2Rest.OAuth2
{
    public interface ITokenService
    {
        TokenServiceProvider TokenServiceProvider { get; set; }
        OAuth2Token GetResourceOwnerToken(ResourceOwnerCredentials resourceOwnerCredentials);
    }
}