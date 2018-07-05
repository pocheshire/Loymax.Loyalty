using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.Colleagues.Items
{
    public class ColleagueItemVm : MvxViewModel
    {
        internal API.Models.Colleague Model { get; }

        private IMvxCommand _giveThanksCommand;
        public IMvxCommand GiveThanksCommand => _giveThanksCommand ?? (_giveThanksCommand = new MvxCommand(OnGiveThanksExecute));

        public string FullName { get; }

        public string RoleName { get; }

        public string ImageUrl { get; }

        public ColleagueItemVm(API.Models.Colleague model)
        {
            Model = model;

            FullName = $"{model.Surname} {model.Name}";
            RoleName = model.RoleName;
            ImageUrl = model.ImageUrl;
        }

        private void OnGiveThanksExecute()
        {
            
        }
    }
}
