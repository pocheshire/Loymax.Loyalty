using System;
using Loyalty.API.Models;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.Colleagues.Items
{
    public class ColleagueItemVm : MvxViewModel
    {
        Colleague Model { get; }

        public ColleagueItemVm(Colleague model)
        {
            Model = model;
        }
    }
}
