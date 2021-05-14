using System;
using System.Threading.Tasks;
using Nginx_Auth.Entity;

namespace Nginx_Auth.Services
{
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly User _defaultUser = new User
        {
            Id = 10001,
            FirstName = "amin",
            LastName = "golmahalleh"
        };

        public Task<User> AuthorizeAsync(string userName, string password)
        {
            if (userName == "admin" && password == "123")
            {
                return Task.FromResult(_defaultUser);
            }

            return Task.FromResult<User>(null);
        }

        public Task<string> GenerateTokenAsync()
        {
            return Task.FromResult(Guid.NewGuid().ToString());
        }

        public Task<User> DeserializeToken(string token)
        {
            if (token.Length == Guid.NewGuid().ToString().Length)
                return Task.FromResult(_defaultUser);

            return Task.FromResult<User>(null);
        }
    }
}