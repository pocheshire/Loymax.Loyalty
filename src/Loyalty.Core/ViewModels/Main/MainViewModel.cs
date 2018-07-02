using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using Loyalty.Core.ViewModels.Colleagues;
using Loyalty.Core.ViewModels.Profile;

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

        #endregion

        #region Services

        #endregion

        #region Constructor

        public MainViewModel()
        {
            SelectedIndex = -1;

            Items = new List<IMvxViewModel>();
        }

        #endregion

        #region Private

        private async void OnSelectedIndexChanged(int index)
        {
            await NavigationService.Navigate(Items[index]);
        }

        #endregion

        #region Protected

        #endregion

        #region Public

        public override Task Initialize()
        {
            Items = new List<IMvxViewModel>
            {
                new ColleaguesViewModel(),
                new ProfileViewModel()
            };

            SelectedIndex = 0;

            return Task.CompletedTask;
        }

        #endregion
    }
}
