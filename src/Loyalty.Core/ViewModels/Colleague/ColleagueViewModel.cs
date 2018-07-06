using System.Threading.Tasks;
using Loyalty.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.Colleague
{
    public class ColleagueViewModel : MvxViewModel<API.Models.Colleague>
    {
        #region Fields

        #endregion

        #region Commands

        private IMvxCommand _giveThanksCommand;
        public IMvxCommand GiveThanksCommand => _giveThanksCommand ?? (_giveThanksCommand = new MvxAsyncCommand(OnGiveThanksExecute));

        #endregion

        #region Properties

        API.Models.Colleague Model { get; set; }

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

        #endregion

        #region Services

        IGiveThanksService GiveThanksService { get; }

        #endregion

        #region Constructor

        public ColleagueViewModel(IGiveThanksService giveThanksService)
        {
            GiveThanksService = giveThanksService;
        }

        #endregion

        #region Private

        private Task OnGiveThanksExecute()
        {
            return GiveThanksService.GiveThanks(Model);
        }

        #endregion

        #region Protected

        protected void LoadContent()
        {
            ImageUrl = Model.ImageUrl;
            FullName = $"{Model.Surname} {Model.Name}";
            RoleName = Model.RoleName;
        }

        #endregion

        #region Public

        public override Task Initialize()
        {
            return Task.Run(() => LoadContent());
        }

        public override void Prepare(API.Models.Colleague parameter)
        {
            Model = parameter;
        }

        #endregion
    }
}
