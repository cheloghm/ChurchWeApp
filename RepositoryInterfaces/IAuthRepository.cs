using ChurchWeApp.Models;

namespace ChurchWeApp.RepositoryInterfaces
{
    public interface IAuthRepository
    {
        Task<string> Login(UserAuthDTO userAuthDto);
        Task<UserDTO> Register(RegisterDTO regDto);
    }
}
