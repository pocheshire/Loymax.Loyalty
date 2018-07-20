using System;
using System.Linq;
using System.Threading.Tasks;
using Loyalty.API.Services;
using Loyalty.Core.Services;
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
        public IMvxCommand SelectionChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = new MvxAsyncCommand<ColleagueItemVm>(OnItemSelected));

        private IMvxCommand _searchCommand;
        public IMvxCommand SearchCommand => _searchCommand ?? (_searchCommand = new MvxAsyncCommand<string>(OnSearchExecute));

        private IMvxCommand _cancelSearchCommand;
        public IMvxCommand CancelSearchCommand => _cancelSearchCommand ?? (_cancelSearchCommand = new MvxAsyncCommand(OnCancelSearch));

        private IMvxCommand _refreshCommand;
        public IMvxCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new MvxAsyncCommand(OnRefreshExecute));

        #endregion

        #region Properties

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value, nameof(Loading));
        }

        private bool _refreshing;
        public bool Refreshing
        {
            get => _refreshing;
            set => SetProperty(ref _refreshing, value, nameof(Refreshing));
        }

        private MvxObservableCollection<ColleagueItemVm> _items;
        public MvxObservableCollection<ColleagueItemVm> Items
        {
            get => _items;
            set => SetProperty(ref _items, value, nameof(Items));
        }

        private bool _isItemsEmpty;
        public bool IsItemsEmpty
        {
            get => _isItemsEmpty;
            set => SetProperty(ref _isItemsEmpty, value, nameof(IsItemsEmpty));
        }

        private bool _hasNoSearchResults;
        public bool HasNoSearchResults
        {
            get => _hasNoSearchResults;
            set => SetProperty(ref _hasNoSearchResults, value, nameof(HasNoSearchResults));
        }

        #endregion

        #region Services

        IColleaguesService ColleagueService { get; }

        IUserDialog UserDialog { get; }

        IGiveThanksService GiveThanksService { get; }

        #endregion

        #region Constructor

        public ColleaguesViewModel(IColleaguesService colleagueService, IUserDialog userDialog, IGiveThanksService giveThanksService)
        {
            ColleagueService = colleagueService;
            UserDialog = userDialog;
            GiveThanksService = giveThanksService;
        }

        #endregion

        #region Private

        private async Task LoadContent(bool refreshing = false)
        {
            if (refreshing)
                Refreshing = true;
            else
                Loading = true;

            HasNoSearchResults = false;

            try
            {
                var rawColleagues = await ColleagueService.GetColleagues();

                Items = new MvxObservableCollection<ColleagueItemVm>(rawColleagues.Select(SetupItem));

                IsItemsEmpty = Items.Count == 0;
            }
            catch (Exception)
            {
                IsItemsEmpty = true;

                UserDialog.ShowAlert("Не удалось загрузить список коллег");
            }

            _initialized = true;

            if (refreshing)
                Refreshing = false;
            else
                Loading = false;
        }

        private async Task SearchContent(string query)
        {
            Loading = true;

            IsItemsEmpty = false;

            try
            {
                var rawColleagues = await ColleagueService.SearchColleagues(query);

                Items = new MvxObservableCollection<ColleagueItemVm>(rawColleagues.Select(SetupItem));

                HasNoSearchResults = Items.Count == 0;
            }
            catch (Exception)
            {
                HasNoSearchResults = true;

                UserDialog.ShowAlert("Не удалось найти коллег по вашему запросу");
            }

            _initialized = true;

            Loading = false;
        }

        private ColleagueItemVm SetupItem(API.Models.Colleague model) => new ColleagueItemVm(model, GiveThanksService);

        private Task OnItemSelected(ColleagueItemVm item)
        {
            //return NavigationService.Navigate<ColleagueViewModel, API.Models.Colleague>(item.Model);
            return Task.CompletedTask;
        }

        private Task OnSearchExecute(string query)
        {
            return SearchContent(query);
        }

        private Task OnCancelSearch()
        {
            return LoadContent();
        }

        private Task OnRefreshExecute()
        {
            return LoadContent(true);
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
