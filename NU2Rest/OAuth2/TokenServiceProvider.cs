namespace NU2Rest.OAuth2
{
    public class TokenServiceProvider
    {
        public TokenServiceProvider(string tokenUrl, string tokenInfoUrl)
        {
            TokenUrl = tokenUrl;
            TokenInfoUrl = tokenInfoUrl;
        }

        public string TokenUrl { get; set; }
        public string TokenInfoUrl { get; set; }
    }
}