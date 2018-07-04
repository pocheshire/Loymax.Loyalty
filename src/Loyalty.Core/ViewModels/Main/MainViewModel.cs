using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.Core.Services;
using Loyalty.Core.ViewModels.Auth;
using Loyalty.Core.ViewModels.Colleagues;
using Loyalty.Core.ViewModels.Profile;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.Main
{
    public class MainViewModel : MvxViewModel
    {
        #region Fields

        #endregion

        #region Commands

        private IMvxCommand _selectionChangedCommand;
        public IMvxCommand SelectionChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = new MvxCommand<int>(index => SelectedIndex = index));

        #endregion

        #region Properties

        private int _selectedIndex;
        int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (SetProperty(ref _selectedIndex, value, nameof(SelectedIndex)))
                    OnSelectedIndexChanged(value);
            }
        }

        private List<IMvxViewModel> _items;
        List<IMvxViewModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value, nameof(Items));
        }

        ISessionService SessionService { get; }

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

        public MainViewModel()
        {
            _selectedIndex = -1;

            Items = new List<IMvxViewModel>();
            SessionService = Mvx.Resolve<ISessionService>();
        }

        #endregion

        #region Private

        private async void OnSelectedIndexChanged(int index)
        {
            await NavigationService.Navigate(Items[index]);
        }

        private async Task LoadContent()
        {
            Loading = true;

            Items = new List<IMvxViewModel>
            {
                new ColleaguesViewModel(),
                new ProfileViewModel()
            };

            var started = await SessionService.StartSession();
            if (started)
            {
                Loading = false;
                SelectedIndex = 0;
                return;
            }

            Loading = false;
        }

        #endregion

        #region Protected

        #endregion

        #region Public

        public override async void ViewCreated()
        {
            base.ViewCreated();

            if (SessionService.IsSignedIn())
                await LoadContent();
            else
            {
                var result = await NavigationService.Navigate<AuthViewModel, bool>();
                if (result)
                    await LoadContent();
            }
        }

        #endregion
    }
}
