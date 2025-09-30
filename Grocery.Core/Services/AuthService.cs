using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;

        private Client? _currentClient;

        public Client? CurrentClient => _currentClient;

        public AuthService(IClientService clientService)
        {

            _clientService = clientService;
        }

        public Client? Login(string email, string password)
        {
            Client? client = _clientService.Get(email);
            if (client == null)
            {
                _currentClient = null;
                return null;
            }
            if (PasswordHelper.VerifyPassword(password, client.Password))
            {
                _currentClient = client;
                return client;
            }

            _currentClient = null;
            return null;
        }
       

        public void Logout()
        {
            _currentClient = null;
        }
    }
}