using System.Threading.Tasks;
using Nginx_Auth.Entity;

namespace Nginx_Auth.Services
{
    public interface IAuthorizationServices
    {
        Task<User> AuthorizeAsync(string userName, string password);
        
        Task<string> GenerateTokenAsync();

        Task<User> DeserializeToken(string token);
    }
}