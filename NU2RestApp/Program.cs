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
            //All created request must use HTTPS
            NU2RestService restService = new NU2RestService(NU2RestScheme.HTTPS);

            const string host = "jsonplaceholder.typicode.com"; //DON'T put the scheme here! E.g HTTPS or HTTP. Instead use the method 'useHttps' or leave the HTTP default scheme.
            const string path = "/posts/:id"; //The path parameter ":id" must be add to 'Params' property. Otherwise, the request will throw an ArgumentNullException

            INU2RestRequest request = restService.CreateRequest(
                host: host,
                path: path);

            //request.UseHttps(); //Only if the server support HTTPS request. Attention: Server certificate must be valid.

            request.Params.Add("id", "1");

            NU2RestResponse<Post> response = request.ReadAsync<Post>().Result;

            Console.WriteLine(response.MetaData.StatusCode);
            Console.WriteLine(response.Data.Title);

            Post post = new Post()
            {
                Body = "xxx",
                Title = "aosdokpa",
                UserId = 2
            };

            NU2RestResponse<Post> response2 = request.UpdateAsync<Post, Post>(post).Result;

            Console.WriteLine(response2.MetaData.StatusCode);
            Console.WriteLine(response2.Data.Title);
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
