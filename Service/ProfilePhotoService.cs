using ChurchWeApp.RepositoryInterfaces;
using ChurchWeApp.ServiceInterfaces;
using System.Threading.Tasks;
using ChurchWeApp.Models;

namespace ChurchWeApp.Service
{
    public class ProfilePhotoService : IProfilePhotoService
    {
        private readonly IProfilePhotoRepository _profilePhotoRepository;

        public ProfilePhotoService(IProfilePhotoRepository profilePhotoRepository)
        {
            _profilePhotoRepository = profilePhotoRepository;
        }

        public async Task<ProfilePhotoDTO> UploadProfilePhoto(byte[] photo)
        {
            return await _profilePhotoRepository.AddProfilePhoto(photo);
        }
    }
}
