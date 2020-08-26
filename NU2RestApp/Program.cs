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
                const string path = "/drive/v1/trash/:trash_id"; //The path parameter ":id" must be add to 'Params' property. Otherwise, the request will throw an ArgumentNullException

                IRestRequest request = restService.CreateRequest(
                    host: host,
                    path: path);

                request.Params.Add("trash_id", "OPKASOKP");

                request.UseBearerAuthentication(access_token: "..");

                Trash trash = new Trash()
                {
                    Kind = "Bla"
                };
                RestResponse<Trash> response = request.UpdateAsync<Trash, Trash>(trash).Result;

                Console.WriteLine(response.MetaData.StatusCode);
            }
            catch (Exception)
            {
                
                
            }
        }
    }

    class Trash
    {
        public string Id { get; set; }
        public string Kind { get; set; }
        public string Name { get; set; }
        public int? Version { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public string[] Labels { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }

    }
}
