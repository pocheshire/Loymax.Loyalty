using System.Linq;
using Android.Animation;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.View.Menu;
using Android.Util;
using Android.Views;
using Android.Widget;
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
        public FrameLayout ContentModalsShadow { get; private set; }

        private void RemoveTextLabels(BottomNavigationView bottomNavigationView)
        {
            var child = bottomNavigationView.GetChildAt(0);

            if (child == null)
                return;

            var menuView = (ViewGroup)child;
            for (int j = 0; j < menuView.ChildCount; j++)
            {
                var viewGroup = (ViewGroup)menuView.GetChildAt(j);
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

        protected override void OnCreate(Bundle bundle)
        {
            SetContentView(Resource.Layout.activity_main);

            var bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation_view);

            bottomNavigationView.SetOnNavigationItemSelectedListener(this);
            bottomNavigationView.SetOnNavigationItemReselectedListener(this);
            (bottomNavigationView.LayoutParameters as CoordinatorLayout.LayoutParams).Behavior = new BottomNavigationViewBehavior();

            ContentModalsShadow = FindViewById<FrameLayout>(Resource.Id.content_modals_shadow_background);

            base.OnCreate(bundle);
        }

        protected override void OnResume()
        {
            base.OnResume();

            var bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation_view);
            RemoveTextLabels(bottomNavigationView);
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            bool handled = false;

            if (keyCode == Keycode.Back)
            {
                if (SupportFragmentManager?.Fragments?.Count == 1 && SupportFragmentManager.Fragments.First() is IBackHandledFragment backHandledFragment)
                    handled = backHandledFragment.OnKeyDown(keyCode, e);
            }

            return handled ? true : base.OnKeyDown(keyCode, e);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            var result = false;

            var userInfoBackground = FindViewById<View>(Resource.Id.profile_user_info_background);

            switch (item.ItemId)
            {
                case Resource.Id.main_tab_colleagues:
                    {
                        result = true;

                        var valueAnimator = ValueAnimator.OfInt((int)Resources.GetDimension(Resource.Dimension.profile_user_info_background_height), 0);
                        valueAnimator.AddUpdateListener(new AnimatorHeightUpdateListener(userInfoBackground));
                        valueAnimator.SetDuration(300);
                        valueAnimator.Start();

                        ViewModel.SelectionChangedCommand.Execute(0);

                        break;
                    }
                case Resource.Id.main_tab_profile:
                    {
                        result = true;

                        var valueAnimator = ValueAnimator.OfInt(0, (int)Resources.GetDimension(Resource.Dimension.profile_user_info_background_height));
                        valueAnimator.AddUpdateListener(new AnimatorHeightUpdateListener(userInfoBackground));
                        valueAnimator.SetDuration(300);
                        valueAnimator.Start();

                        ViewModel.SelectionChangedCommand.Execute(1);

                        break;
                    }
            }

            return result;
        }

        public void OnNavigationItemReselected(IMenuItem item)
        {

        }

        private class AnimatorHeightUpdateListener : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
        {
            private readonly View view;

            public AnimatorHeightUpdateListener(View view)
            {
                this.view = view;
            }
            public void OnAnimationUpdate(ValueAnimator animation)
            {
                view.LayoutParameters.Height = (int)animation.AnimatedValue;
                view.RequestLayout();
            }
        }
    }
}
