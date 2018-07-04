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

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value, nameof(Loading));
        }

        #endregion

        #region Services

        ISessionService SessionService { get; }

        IMvxViewModelLoader ViewModelLoader { get; }

        #endregion

        #region Constructor

        public MainViewModel(IMvxViewModelLoader viewModelLoader)
        {
            _selectedIndex = -1;

            Items = new List<IMvxViewModel>();

            SessionService = Mvx.Resolve<ISessionService>();
            ViewModelLoader = viewModelLoader;
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

            var started = await SessionService.StartSession();
            if (started)
            {
                Items = new List<IMvxViewModel>
                {
                    ViewModelLoader.LoadViewModel(MvxViewModelRequest.GetDefaultRequest(typeof(ColleaguesViewModel)), null),
                    ViewModelLoader.LoadViewModel(MvxViewModelRequest.GetDefaultRequest(typeof(ProfileViewModel)), null)
                };

                SelectedIndex = 0;
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
