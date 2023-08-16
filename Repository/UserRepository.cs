using System.Net.Http;
using ChurchWeApp.Models;
using ChurchWeApp.RepositoryInterfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ChurchWeApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;

        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDTO> GetMyDetails()
        {
            // Add the authorization header with the token.
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
            {
                // Fetch the token from where you store it after login (for example, in a service or configuration).
                string token = "<YOUR_TOKEN_HERE>";
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

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
