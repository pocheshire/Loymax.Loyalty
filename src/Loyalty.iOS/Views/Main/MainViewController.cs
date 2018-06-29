using Loyalty.Core.ViewModels.Main;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;

namespace Loyalty.iOS.Views.Main
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class MainViewController : MvxTabBarViewController<MainViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<MainViewController, MainViewModel>();

            set.Apply();
        }
    }
}

