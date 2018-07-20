using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Loyalty.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Loyalty.API.Services;
using Loyalty.API.Models;
using Loyalty.Core.ViewModels.Profile.Items;
using System.Linq;

namespace Loyalty.Core.ViewModels.Profile
{
    public class ProfileViewModel : MvxViewModel
    {
        #region Fields

        private bool _initialized;

        #endregion

        #region Commands

        private IMvxCommand _moreAchievementsCommand;
        public IMvxCommand MoreAchievementsCommand => _moreAchievementsCommand ?? (_moreAchievementsCommand = new MvxCommand(() => { }));

        private IMvxCommand _moreThanksCommand;
        public IMvxCommand MoreThanksCommand => _moreThanksCommand ?? (_moreThanksCommand = new MvxCommand(() => { }));

        #endregion

        #region Properties

        public string Id { get; private set; }

        public List<ITransformation> Transformations => new List<ITransformation> { new CircleTransformation() };

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

        private string _balanceString;
        public string BalanceString
        {
            get => _balanceString;
            set => SetProperty(ref _balanceString, value, nameof(BalanceString));
        }

        private bool _hasRecievedThanks;
        public bool HasRecievedThanks
        {
            get => _hasRecievedThanks;
            set => SetProperty(ref _hasRecievedThanks, value, nameof(HasRecievedThanks));
        }

        private bool _moreAchievementsVisible;
        public bool MoreAchievementsVisible
        {
            get => _moreAchievementsVisible;
            set => SetProperty(ref _moreAchievementsVisible, value, nameof(MoreAchievementsVisible));
        }

        private bool _moreThanksVisible;
        public bool MoreThanksVisible
        {
            get => _moreThanksVisible;
            set => SetProperty(ref _moreThanksVisible, value, nameof(MoreThanksVisible));
        }

        private MvxObservableCollection<AchievementItemVm> _achievementItems;
        public MvxObservableCollection<AchievementItemVm> AchievementItems
        {
            get => _achievementItems;
            set => SetProperty(ref _achievementItems, value, nameof(AchievementItems));
        }

        #endregion

        #region Services

        ISessionService SessionService { get; }

        IUserDialog UserDialog { get; }

        IAchievementsService AchievementsService { get; }

        IRecievedThanksService RecievedThanksService { get; }

        #endregion

        #region Constructor

        public ProfileViewModel(IUserDialog userDialog, IAchievementsService achievementsService, IRecievedThanksService recievedThanksService)
        {
            SessionService = Mvx.Resolve<ISessionService>();
            UserDialog = userDialog;
            AchievementsService = achievementsService;
            RecievedThanksService = recievedThanksService;
        }

        #endregion

        #region Private

        private async Task LoadContent()
        {
            var user = SessionService.GetUser();

            if (!_initialized)
            {
                Id = user.Id;
                ImageUrl = user.ImageUrl;
                FullName = $"{user.Name}\n{user.Surname}";
            }

            BalanceString = ToBalanceString(user.Balance);

            await Task.WhenAll(LoadAchevements(user), LoadRecievedThanks(user));
        }

        private string ToBalanceString(decimal balance)
        {
            return balance.ToString("C0", new CultureInfo("ru-RU").NumberFormat);
        }

        private async Task LoadAchevements(User user)
        {
            try
            {
                var achievementsRaw = await AchievementsService.GetAchievements(user.Id);

                AchievementItems = new MvxObservableCollection<AchievementItemVm>(achievementsRaw.Select(SetupAchievementItem));

                MoreAchievementsVisible = AchievementItems.Count > 3;
            }
            catch (Exception)
            {
                MoreAchievementsVisible = false;
            }
        }

        private AchievementItemVm SetupAchievementItem(Achievement model)
            => new AchievementItemVm(model);

        private Task LoadRecievedThanks(User user)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Protected

        #endregion

        #region Public

        public override Task Initialize()
        {
            return LoadContent();
        }

        #endregion
    }
}
