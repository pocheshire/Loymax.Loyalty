using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Loyalty.Core.ViewModels.Main;
using Loyalty.Droid.Behaviors;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.Main
{
    [MvxActivityPresentation]
    [Activity(
        ScreenOrientation = ScreenOrientation.Portrait
        , Theme = "@style/AppTheme"
        , WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>, BottomNavigationView.IOnNavigationItemSelectedListener, BottomNavigationView.IOnNavigationItemReselectedListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.activity_main);

            var bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation_view);

            bottomNavigationView.SetOnNavigationItemSelectedListener(this);
            bottomNavigationView.SetOnNavigationItemReselectedListener(this);
            (bottomNavigationView.LayoutParameters as CoordinatorLayout.LayoutParams).Behavior = new BottomNavigationViewBehavior();

            base.OnCreate(bundle);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            var result = false;

            switch (item.ItemId)
            {
                case Resource.Id.main_tab_colleagues:
                    result = true;
                    ViewModel.SelectionChangedCommand.Execute(0);
                    break;
                case Resource.Id.main_tab_profile:
                    result = true;
                    ViewModel.SelectionChangedCommand.Execute(1);
                    break;
            }

            return result;
        }

        public void OnNavigationItemReselected(IMenuItem item)
        {

        }
    }
}
