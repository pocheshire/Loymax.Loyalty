using System;
using System.Threading.Tasks;
using Loyalty.API.Models;
using System.Security.Authentication;

namespace Loyalty.API.Services.Mocks
{
    public class MockAuthService : IAuthService
    {
        const string USERNAME = "admin";
        const string PASSWORD = "111111";

        public Task<string> SignIn(string username, string password)
        {
            if (username == USERNAME && password == PASSWORD)
                return Task.FromResult(Guid.NewGuid().ToString());

            throw new AuthenticationException("Неверный логин и/или пароль");
        }

        public Task<User> GetUser(string token)
        {
            if (!string.IsNullOrEmpty(token))
                return Task.FromResult(CreateUser());

            throw new ArgumentNullException(nameof(token));
        }

        private User CreateUser()
        {
            return new User
            {
                Id = Guid.NewGuid().ToString(),
                Surname = "Константинопольский",
                Name = "Константин",
                MiddleName = "Константинович",
                RoleName = "Техническая поддержка",
                Balance = 1000,
                ImageUrl = "https://image.ibb.co/iS2s4y/images_2.png"
            };
        }
    }
}
