using System.Net;
using RestOn.Auth;
using RestOn.Service;

namespace RestOn.Sample;

public class BackendForFrontendProxy: IOpenIdConnectProvider
{
    private readonly IRestService apiService;
    public Uri TokenUri { get; set; }
    
    public BackendForFrontendProxy(Uri tokenUri, IRestService apiService)
    {
        this.TokenUri = tokenUri;
        this.apiService = apiService;
    }
    
    public BackendForFrontendProxy(string tokenUri, IRestService apiService): this(new Uri(tokenUri), apiService)
    {
    }
    
    public async Task<OAuth2Token> ExchangeForToken(OpenIdConnectCredentials oidcCredentials)
    {
        var data = new Dictionary<string, string>();
        data.Add("code", oidcCredentials.Code);
        data.Add("redirect_uri", oidcCredentials.RedirectUri);
        
        return await GetToken(data);
    }
    
    public async Task<OAuth2Token> GetToken(OAuth2Token token)
    {
        var data = new Dictionary<string, string>();
        data.Add("refresh_token", token.RefreshToken);

        return await GetToken(data);
    }
    private async Task<OAuth2Token> GetToken(Dictionary<string, string> data)
    {
        var response = await apiService
            .CreateRequest(TokenUri)
            .PostAsync<OAuth2Token>(data, HttpStatusCode.OK);

        return response.Data;
    }
}