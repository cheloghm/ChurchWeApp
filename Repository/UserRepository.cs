using System.Net.Http;
using ChurchWeApp.Models;
using ChurchWeApp.RepositoryInterfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ChurchWeApp.ServiceInterfaces;

namespace ChurchWeApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public UserRepository(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<UserDTO> GetMyDetails()
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("No authentication token found.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("Users/me");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();
            var userDto = JsonConvert.DeserializeObject<UserDTO>(result);

            return userDto;
        }
    }

}
