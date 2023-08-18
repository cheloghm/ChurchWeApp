using ChurchWeApp.Models;

namespace ChurchWeApp.ServiceInterfaces
{
    public interface IProfilePhotoService
    {
        Task<ProfilePhotoDTO> UploadProfilePhoto(byte[] photo);
    }
}
