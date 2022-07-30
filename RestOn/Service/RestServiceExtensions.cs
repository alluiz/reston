using Microsoft.Extensions.DependencyInjection;
using RestOn.Auth;
using RestOn.Http;
using System.Security.Cryptography.X509Certificates;

namespace RestOn.Service
{
    public static class RestServiceExtensions
    {
        public static IServiceCollection AddRestService(this IServiceCollection services)
        {
            services.AddSingleton<IRestService, RestService>();

            return services;
        }

        public static IServiceCollection AddRestService(this IServiceCollection services, RestScheme scheme)
        {
            services.AddSingleton<IRestService>(x => new RestService(scheme));

            return services;
        }

        public static IServiceCollection AddTokenService(this IServiceCollection services, TokenServiceProvider tokenServiceProvider)
        {
            services.AddSingleton<ITokenService>(x => new TokenService(tokenServiceProvider));

            return services;
        }
    }
}