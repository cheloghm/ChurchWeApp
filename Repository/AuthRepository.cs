using ChurchWeApp.Models;
using ChurchWeApp.RepositoryInterfaces;

namespace ChurchWeApp.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly HttpClient _httpClient;

        public AuthRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(UserAuthDTO userAuthDto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", userAuthDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
                return result["token"];
            }

            return null;
        }

        public async Task<UserDTO> Register(RegisterDTO regDto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", regDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UserDTO>();
            }
            return null;
        }

    }
}
