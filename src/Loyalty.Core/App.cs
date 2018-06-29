using System;
using MvvmCross.ViewModels;
using Loyalty.Core.ViewModels.Main;

namespace Loyalty.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MainViewModel>();
        }
    }
}
