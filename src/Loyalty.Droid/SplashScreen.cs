using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Loyalty.Droid
{
    [Activity(
        Label = "@string/app_name"
        , MainLauncher = true
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenAppCompatActivity<MvxAppCompatSetup<Core.App>, Core.App>
    {
        public SplashScreen()
            : base (Resource.Layout.activity_splash)
        {
            
        }
    }
}
