using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Loyalty.Core.ViewModels.Main;
using Loyalty.Droid.Behaviors;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Support.V7.View.Menu;
using Android.Util;

namespace Loyalty.Droid.Views.Main
{
    [MvxActivityPresentation]
    [Activity(
        ScreenOrientation = ScreenOrientation.Portrait
        , Theme = "@style/AppTheme"
        , WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>, BottomNavigationView.IOnNavigationItemSelectedListener, BottomNavigationView.IOnNavigationItemReselectedListener
    {
        private void RemoveTextLabels(BottomNavigationView bottomNavigationView, int menuResId)
        {
            var child = bottomNavigationView.GetChildAt(0);

            if (child == null)
                return;

            if (child is IMenuView)
            {
                var menuView = (ViewGroup)child;
                for (int j = 0; j < menuView.ChildCount; j++)
                {
                    var view = menuView.GetChildAt(j);

                    if (view is IMenuViewItemView)
                    {
                        var viewGroup = (ViewGroup)view;
                        for (int i = 0; i < viewGroup.ChildCount; i++)
                        {
                            var v = viewGroup.GetChildAt(i);
                            if (v is ViewGroup)
                            {
                                viewGroup.RemoveViewAt(i);
                            }
                        }

                        var padding = TypedValue.ApplyDimension(ComplexUnitType.Dip, 16, Resources.DisplayMetrics);

                        viewGroup.SetPadding(viewGroup.PaddingLeft, (int)padding / 2, viewGroup.PaddingRight, viewGroup.PaddingBottom);
                    }
                }

            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.activity_main);

            var bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation_view);

            bottomNavigationView.SetOnNavigationItemSelectedListener(this);
            bottomNavigationView.SetOnNavigationItemReselectedListener(this);
            (bottomNavigationView.LayoutParameters as CoordinatorLayout.LayoutParams).Behavior = new BottomNavigationViewBehavior();

            base.OnCreate(bundle);
        }

        protected override void OnResume()
        {
            base.OnResume();

            var bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation_view);

            RemoveTextLabels(bottomNavigationView, Resource.Menu.menu_bottom_navigation);
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
