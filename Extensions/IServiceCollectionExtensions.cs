using Microsoft.Extensions.DependencyInjection;
using ChurchWeApp.ServiceInterfaces;
using ChurchWeApp.Service;
using ChurchWeApp.RepositoryInterfaces;
using ChurchWeApp.Repository;
using System.Net.Http;

namespace ChurchWeApp.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Register a named HttpClient with the base address
            services.AddHttpClient("DefaultHttpClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5159/api/");
            });

            // Register repositories with the named HttpClient
            services.AddTransient<IAuthRepository>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("DefaultHttpClient");
                return new AuthRepository(httpClient);
            });

            // Register UserRepository in the same way, using the same named HttpClient
            services.AddTransient<IUserRepository>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("DefaultHttpClient");
                return new UserRepository(httpClient);
            });

            return services; // Return the services for chaining if needed.
        }
    }
}
