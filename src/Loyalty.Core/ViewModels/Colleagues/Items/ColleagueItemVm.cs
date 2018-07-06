using System.Threading.Tasks;
using Loyalty.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.Colleagues.Items
{
    public class ColleagueItemVm : MvxViewModel
    {
        internal API.Models.Colleague Model { get; }

        private IMvxCommand _giveThanksCommand;
        public IMvxCommand GiveThanksCommand => _giveThanksCommand ?? (_giveThanksCommand = new MvxAsyncCommand(OnGiveThanksExecute));

        public string FullName { get; }

        public string RoleName { get; }

        public string ImageUrl { get; }

        IGiveThanksService GiveThanksService { get; }

        public ColleagueItemVm(API.Models.Colleague model, IGiveThanksService giveThanksService)
        {
            Model = model;
            GiveThanksService = giveThanksService;
            FullName = $"{model.Surname} {model.Name}";
            RoleName = model.RoleName;
            ImageUrl = model.ImageUrl;
        }

        private Task OnGiveThanksExecute()
        {
            return GiveThanksService.GiveThanks(Model);
        }
    }
}
