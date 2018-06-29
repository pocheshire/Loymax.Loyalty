using Android.App;
using Android.OS;
using Loyalty.Core.ViewModels.Main;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.Main
{
    [MvxActivityPresentation]
    [Activity]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
        }
    }
}
