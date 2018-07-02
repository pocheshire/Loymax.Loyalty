using System.Threading.Tasks;
using Loyalty.API.Models;

namespace Loyalty.API.Services
{
    public interface IAuthService
    {
        Task<string> SignIn(string username, string password);

        Task<User> GetUser(string token);
    }
}
