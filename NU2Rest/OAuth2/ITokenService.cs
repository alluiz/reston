namespace NU2Rest.OAuth2
{
    public interface ITokenService
    {
        string Url { get; set; }

        IRestRequest CreateRequest();
        IRestRequest CreateRequest(string url);
        OAuth2Token GetResourceOwnerToken(ResourceOwnerCredentials resourceOwnerCredentials);
    }
}