using System;
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Loyalty.Core.Services;
using MvvmCross;
using System.Globalization;

namespace Loyalty.Core.ViewModels.Profile
{
    public class ProfileViewModel : MvxViewModel
    {
        #region Fields

        private bool _initialized;

        #endregion

        #region Commands

        #endregion

        #region Properties

        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value, nameof(ImageUrl));
        }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value, nameof(FullName));
        }

        private string _roleName;
        public string RoleName
        {
            get => _roleName;
            set => SetProperty(ref _roleName, value, nameof(RoleName));
        }

        private string _balanceString;
        public string BalanceString
        {
            get => _balanceString;
            set => SetProperty(ref _balanceString, value, nameof(BalanceString));
        }

        #endregion

        #region Services

        ISessionService SessionService { get; }

        IUserDialog UserDialog { get; }

        #endregion

        #region Constructor

        public ProfileViewModel(IUserDialog userDialog)
        {
            SessionService = Mvx.Resolve<ISessionService>();
            UserDialog = userDialog;
        }

        #endregion

        #region Private

        private void LoadContent()
        {
            var user = SessionService.GetUser();

            if (!_initialized)
            {
                ImageUrl = user.ImageUrl;
                FullName = $"{user.Surname} {user.Name}";
                RoleName = user.RoleName;
            }

            BalanceString = ToBalanceString(user.Balance);
        }

        private string ToBalanceString(decimal balance)
        {
            return balance.ToString("C0", CultureInfo.CurrentUICulture.NumberFormat);
        }

        #endregion

        #region Protected

        #endregion

        #region Public

        public override Task Initialize()
        {
            return Task.Run(() => LoadContent());
        }

        #endregion
    }
}
