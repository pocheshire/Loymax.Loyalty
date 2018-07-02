using System;
using System.Threading.Tasks;
using Loyalty.API.Services;
using Loyalty.API.Models;
using Plugin.SecureStorage;

namespace Loyalty.Core.Services.Implementations
{
    class SessionService : ISessionService
    {
        const string TOKEN_KEY = "loyalty.session.token";
        
        User User { get; set; }

        IAuthService AuthService { get; }
        IUserDialog UserDialog { get; }

        public SessionService(IAuthService authService, IUserDialog userDialog)
        {
            AuthService = authService;
            UserDialog = userDialog;
        }

        public async Task Init(string username, string password)
        {
            try
            {
                var token = await AuthService.SignIn(username, password);

                User = await AuthService.GetUser(token);

                SaveToken(token);
            }
            catch (Exception ex)
            {
                UserDialog.ShowAlert(ex.Message);
            }
        }

        public bool IsSignedIn()
        {
            return CrossSecureStorage.Current.HasKey(TOKEN_KEY);
        }

        public async Task<bool> StartSession()
        {
            var result = false;
            try
            {
                User = await AuthService.GetUser(CrossSecureStorage.Current.GetValue(TOKEN_KEY));
                result = true;
            }
            catch (Exception ex)
            {
                UserDialog.ShowAlert(ex.Message);
            }
            return result;
        }

        public Task StopSession()
        {
            return Task.Run(() =>
            {
                User = null;
                CrossSecureStorage.Current.DeleteKey(TOKEN_KEY);
            });
        }

        public User GetUser()
        {
            return User;
        }

        private void SaveToken(string token)
        {
            CrossSecureStorage.Current.SetValue(TOKEN_KEY, token);
        }
    }
}
