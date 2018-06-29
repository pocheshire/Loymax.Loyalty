using Foundation;
using MvvmCross.Platforms.Ios.Core;

namespace Loyalty.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<MvxIosSetup<Core.App>, Core.App>
    {
    }
}

