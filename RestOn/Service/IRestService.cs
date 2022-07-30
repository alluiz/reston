using RestOn.Http;

namespace RestOn.Service
{
    public interface IRestService
    {
        /// <summary>
        /// The scheme will be used in the requests. HTTP (default) or HTTPS
        /// </summary>
        RestScheme Scheme { get; set; }

        /// <summary>
        /// Create RestRequest instance
        /// </summary>
        /// <param name="host">The host server. Eg.: domain.com</param>
        /// <param name="path">The path param. Eg.: /test</param>
        /// <returns>A new RestRequest instance</returns>
        IRestRequest CreateRequest(string host, string path);

        /// <summary>
        /// Create RestRequest instance
        /// </summary>
        /// <param name="url">The full URL. Eg.: https://domain.com/test</param>
        /// <returns>A new RestRequest instance</returns>
        IRestRequest CreateRequest(string url);

        /// <summary>
        /// Create RestRequest instance
        /// </summary>
        /// <param name="host">The host server. Eg.: domain.com</param>
        /// <param name="port">The server port. Eg.: 443</param>
        /// <param name="path">The path param. Eg.: /test</param>
        /// <returns>A new RestRequest instance</returns>
        IRestRequest CreateRequest(string host, int port, string path);
    }
}