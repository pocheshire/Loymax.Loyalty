using System.Collections.Generic;
using System.Threading.Tasks;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Loyalty.API.Models;
using Loyalty.Core.ViewModels.GiveThanks;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross;
using MvvmCross.Navigation;

namespace Loyalty.Core.ViewModels.Colleagues.Items
{
    public class ColleagueItemVm : MvxViewModel
    {
        internal Colleague Model { get; }

        private IMvxCommand _giveThanksCommand;
        public IMvxCommand GiveThanksCommand => _giveThanksCommand ?? (_giveThanksCommand = new MvxAsyncCommand(OnGiveThanksExecute));

        public string Id { get; }

        public string FullName { get; }

        public string RoleName { get; }

        public string ImageUrl { get; }

        public List<ITransformation> Transformations => new List<ITransformation> { new CircleTransformation() };

        public ColleagueItemVm(Colleague model)
        {
            Model = model;

            NavigationService = Mvx.Resolve<IMvxNavigationService>();

            Id = model.Id;
            FullName = $"{model.Surname} {model.Name}";
            RoleName = model.RoleName;
            ImageUrl = model.ImageUrl;
        }

        private Task OnGiveThanksExecute()
        {
            return NavigationService.Navigate<GiveThanksViewModel, Colleague>(Model);
        }
    }
}
