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
using Loyalty.Core.Services;

namespace Loyalty.Core.ViewModels.Colleagues.Items
{
    public class ColleagueItemVm : MvxViewModel
    {
        internal Colleague Model { get; }

        ISessionService SessionService { get; }

        IUserDialog UserDialog { get; }

        private IMvxCommand _giveThanksCommand;
        public IMvxCommand GiveThanksCommand => _giveThanksCommand ?? (_giveThanksCommand = new MvxAsyncCommand(OnGiveThanksExecute));

        public string Id { get; }

        public string FullName { get; }

        public string RoleName { get; }

        public string ImageUrl { get; }

        public List<ITransformation> Transformations => new List<ITransformation> { new CircleTransformation() };

        public ColleagueItemVm(Colleague model, IUserDialog userDialog)
        {
            Model = model;

            UserDialog = userDialog;
            SessionService = Mvx.Resolve<ISessionService>();
            NavigationService = Mvx.Resolve<IMvxNavigationService>();

            Id = model.Id;
            FullName = $"{model.Surname} {model.Name}";
            RoleName = model.RoleName;
            ImageUrl = model.ImageUrl;
        }

        private Task OnGiveThanksExecute()
        {
            var user = SessionService.GetUser();
            if (user.Balance <= 0)
            {
                UserDialog.ShowAlert("Вы уже отблагодарили своих коллег сполна, спасибо!");

                return Task.CompletedTask;
            }

            return NavigationService.Navigate<GiveThanksViewModel, Colleague>(Model);
        }
    }
}
