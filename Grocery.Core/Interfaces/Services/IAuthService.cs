using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Client? CurrentClient { get; }

        Client? Login(string email, string password);
        void Logout();
    }
}