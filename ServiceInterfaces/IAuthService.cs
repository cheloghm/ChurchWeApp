namespace ChurchWeApp.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);
        Task<bool> Register(string firstName, string middleName, string lastName, string email, string role, DateTime dob, string password);

    }
}
