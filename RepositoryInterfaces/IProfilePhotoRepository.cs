using ChurchWeApp.Models;

namespace ChurchWeApp.RepositoryInterfaces
{
    public interface IProfilePhotoRepository
    {
        Task<ProfilePhotoDTO> AddProfilePhoto(byte[] photo);
    }
}
