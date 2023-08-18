using ChurchWeApp.Models;
using ChurchWeApp.RepositoryInterfaces;
using ChurchWeApp.ServiceInterfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChurchWeApp.Repository
{
    public class ProfilePhotoRepository : IProfilePhotoRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public ProfilePhotoRepository(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<ProfilePhotoDTO> AddProfilePhoto(byte[] photo)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("No authentication token found.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var content = new MultipartFormDataContent();
            var byteArrayContent = new ByteArrayContent(photo);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            content.Add(byteArrayContent, "photo", "profile_photo.jpg");

            var response = await _httpClient.PostAsync("ProfilePhotos", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();
            var profilePhoto = JsonConvert.DeserializeObject<ProfilePhotoDTO>(result);

            return profilePhoto;
        }

    }
}
