using System.Threading.Tasks;
using Loyalty.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.Auth
{
    public class AuthViewModel : MvxViewModelResult<bool>
    {
        #region Fields

        #endregion

        #region Commands

        private IMvxCommand _signInCommand;
        public IMvxCommand SignInCommand => _signInCommand ?? (_signInCommand = new MvxAsyncCommand(OnSignInExecute));

        internal ISessionService SessionService { get; }

        #endregion

        #region Properties

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value, nameof(Username));
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(Password));
        }

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value, nameof(Loading));
        }

        #endregion

        #region Services

        #endregion

        #region Constructor

        public AuthViewModel()
        {
            SessionService = Mvx.Resolve<ISessionService>();
        }

        #endregion

        #region Private

        private async Task OnSignInExecute()
        {
            Loading = true;

            var result = await SessionService.Init(Username, Password);

            Loading = false;

            if (result)
                await NavigationService.Close(this, true);
        }

        #endregion
    }
}
