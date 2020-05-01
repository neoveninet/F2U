using System.Threading.Tasks;
using f2u.API.Models;

namespace f2u.API.Data
{
    public interface IAuthenticationRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);

        Task<bool> UserExists(string userName);
    }
}