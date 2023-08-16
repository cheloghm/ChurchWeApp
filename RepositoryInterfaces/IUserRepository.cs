using ChurchWeApp.Models;

namespace ChurchWeApp.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<UserDTO> GetMyDetails();
    }
}
