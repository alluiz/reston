namespace NU2Rest.OAuth2
{
    public class OAuth2Credentials
    {
        private readonly string grant_type;

        public OAuth2Credentials(string grant_type)
        {
            this.grant_type = grant_type;
        }

        public string Grant_type => grant_type;

        public string Scope { get; set; }
    }
}