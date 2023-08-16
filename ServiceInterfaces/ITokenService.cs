namespace ChurchWeApp.ServiceInterfaces
{
    public interface ITokenService
    {
        string GetToken();
        void SetToken(string token);
        void ClearToken();
        bool IsLoggedIn();
    }
}
