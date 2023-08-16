using ChurchWeApp.Models;
using ChurchWeApp.RepositoryInterfaces;
using ChurchWeApp.ServiceInterfaces;
using System.Threading.Tasks;

namespace ChurchWeApp.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetMyDetails()
        {
            return await _userRepository.GetMyDetails();
        }
    }
}
