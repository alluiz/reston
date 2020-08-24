using System;
using System.Collections.Generic;
using NU2Rest;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace NU2RestApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // IServiceCollection services = new ServiceCollection().AddHttpClient();

            // services.AddNU2RestService();

            // IServiceProvider provider = services.BuildServiceProvider();

            RunApp();
        }

        private static void RunApp()
        {
            try
            {
                //All created request must use HTTPS
                RestService restService = new RestService(RestScheme.HTTPS);

                const string host = "cdws.us-east-1.amazonaws.com"; //DON'T put the scheme here! E.g HTTPS or HTTP. Instead use the method 'useHttps' or leave the HTTP default scheme.
                const string path = "/drive/v1/trash"; //The path parameter ":id" must be add to 'Params' property. Otherwise, the request will throw an ArgumentNullException

                IRestRequest request = restService.CreateRequest(
                    host: host,
                    path: path);

                //request.UseHttps(); //Only if the server support HTTPS request. Attention: Server certificate must be valid.

                RestResponse<Post> response = request.ReadAsync<Post>().Result;

                Console.WriteLine(response.MetaData.StatusCode);
            }
            catch (Exception)
            {
                
                
            }
        }
    }

    public class Post
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int? UserId { get; set; }
    }
}
