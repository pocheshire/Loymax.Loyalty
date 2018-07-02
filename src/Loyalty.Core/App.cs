using Loyalty.Core.ViewModels.Main;
using MvvmCross.ViewModels;
using MvvmCross;
using Loyalty.Core.Services;
using Loyalty.Core.Services.Implementations;

namespace Loyalty.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            new API.App().Initialize();

            Mvx.LazyConstructAndRegisterSingleton<ISessionService, SessionService>();

            RegisterAppStart<MainViewModel>();
        }
    }
}
