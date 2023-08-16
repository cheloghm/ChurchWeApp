using ChurchWeApp.ServiceInterfaces;

namespace ChurchWeApp.Service
{
    public class TokenService : ITokenService
    {
        private string _token;

        public string GetToken()
        {
            return _token;
        }

        public void SetToken(string token)
        {
            _token = token;
        }

        public void ClearToken()
        {
            _token = null;
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(_token);
        }
    }
}
