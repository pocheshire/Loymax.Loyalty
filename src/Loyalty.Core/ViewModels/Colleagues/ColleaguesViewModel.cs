using System;
using System.Linq;
using System.Threading.Tasks;
using Loyalty.API.Services;
using Loyalty.Core.Services;
using Loyalty.Core.ViewModels.Colleague;
using Loyalty.Core.ViewModels.Colleagues.Items;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.Colleagues
{
    public class ColleaguesViewModel : MvxViewModel
    {
        #region Fields

        private bool _initialized;

        #endregion

        #region Commands

        private IMvxCommand _selectionChangedCommand;
        public IMvxCommand SelecitonChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = new MvxAsyncCommand<ColleagueItemVm>(OnItemSelected));

        #endregion

        #region Properties

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value, nameof(Loading));
        }

        private MvxObservableCollection<ColleagueItemVm> _items;
        public MvxObservableCollection<ColleagueItemVm> Items
        {
            get => _items;
            set => SetProperty(ref _items, value, nameof(Items));
        }

        #endregion

        #region Services

        IColleaguesService ColleagueService { get; }

        IUserDialog UserDialog { get; }

        #endregion

        #region Constructor

        public ColleaguesViewModel(IColleaguesService colleagueService, IUserDialog userDialog)
        {
            ColleagueService = colleagueService;
            UserDialog = userDialog;
        }

        #endregion

        #region Private

        private async Task LoadContent()
        {
            Loading = true;

            try
            {
                var rawColleagues = await ColleagueService.GetColleagues();

                Items = new MvxObservableCollection<ColleagueItemVm>(rawColleagues.Select(SetupItem));
            }
            catch (Exception)
            {
                UserDialog.ShowAlert("Не удалось загрузить список коллег");
            }

            _initialized = true;

            Loading = false;
        }

        private ColleagueItemVm SetupItem(API.Models.Colleague model) => new ColleagueItemVm(model);

        private Task OnItemSelected(ColleagueItemVm item)
        {
            return NavigationService.Navigate<ColleagueViewModel, API.Models.Colleague>(item.Model);
        }

        #endregion

        #region Protected

        #endregion

        #region Public

        public override Task Initialize()
        {
            return _initialized ? Task.CompletedTask : LoadContent();
        }

        #endregion
    }
}
