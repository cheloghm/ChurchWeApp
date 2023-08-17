using ChurchWeApp.Models;

namespace ChurchWeApp.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetMyDetails();
        Task<UserDTO> UpdateUserProfile(UpdateUserDTO userDto);
    }
}
