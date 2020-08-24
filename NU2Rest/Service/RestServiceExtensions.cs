using Microsoft.Extensions.DependencyInjection;

namespace NU2Rest
{
    public static class RestServiceExtensions
    {
        public static IServiceCollection AddNU2RestService(this IServiceCollection services)
        {
            return services;
        }
    }
}