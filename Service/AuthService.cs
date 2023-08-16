using ChurchWeApp.Models;
using ChurchWeApp.RepositoryInterfaces;
using ChurchWeApp.ServiceInterfaces;

namespace ChurchWeApp.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<string> Login(string email, string password)
        {
            var userAuthDto = new UserAuthDTO { Email = email, Password = password };
            return await _authRepository.Login(userAuthDto);
        }

        // ...

        public async Task<bool> Register(string firstName, string middleName, string lastName, string email, string role, DateTime dob, string password)
        {
            var registerDto = new RegisterDTO
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Email = email,
                Role = role,
                DOB = dob,
                Password = password
            };

            return await _authRepository.Register(registerDto) != null;
        }

    }

}
