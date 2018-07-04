using System;
using System.Linq;
using System.Threading.Tasks;
using Loyalty.API.Models;
using Loyalty.API.Services;
using Loyalty.Core.Services;
using Loyalty.Core.ViewModels.Colleagues.Items;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.Colleagues
{
    public class ColleaguesViewModel : MvxViewModel
    {
        #region Fields

        private bool _initialized;

        #endregion

        #region Commands

        #endregion

        #region Properties

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
        }

        private ColleagueItemVm SetupItem(Colleague model) => new ColleagueItemVm(model);

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
