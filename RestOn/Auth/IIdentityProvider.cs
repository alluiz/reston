using System.Threading.Tasks;

namespace RestOn.Auth
{
    public interface IIdentityProvider: IOpenIdConnectProvider, IResouceOwnerProvider, ITokenInstrospection
    {
        /// <summary>
        /// The client credentials
        /// </summary>
        public OAuth2Credentials Credentials { get; set; }
    }
}