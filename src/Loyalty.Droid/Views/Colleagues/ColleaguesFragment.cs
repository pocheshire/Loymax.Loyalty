using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Loyalty.Core.ViewModels.Colleagues;
using Loyalty.Core.ViewModels.Main;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Util;

namespace Loyalty.Droid.Views.Colleagues
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false, Resource.Animation.abc_popup_enter, Resource.Animation.abc_popup_exit, isCacheableFragment: true)]
    public class ColleaguesFragment : MvxFragment<ColleaguesViewModel>
    {
        public ColleaguesFragment()
        {
        }

        protected ColleaguesFragment(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_colleagues, null);

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var appBarLayout = view.FindViewById<AppBarLayout>(Resource.Id.fragment_colleagues_appBarLayout);
            var initialElevation = 6;
            appBarLayout.Elevation = 0;

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.fragment_colleagues_recyclerView);
            var initialTopPosition = TypedValue.ApplyDimension(ComplexUnitType.Dip, -4, Resources.DisplayMetrics);

            recyclerView.AddOnScrollListener(new OnScrollListener(appBarLayout, initialElevation, (int)initialTopPosition));
        }

        private class OnScrollListener : RecyclerView.OnScrollListener
        {
            private readonly AppBarLayout _appBarLayout;
            private readonly float _initialElevation;
            private readonly int _initialTopPosition;

            public OnScrollListener(AppBarLayout appBarLayout, float initialElevation, int initialTopPosition)
            {
                _appBarLayout = appBarLayout;
                _initialElevation = initialElevation;
                _initialTopPosition = initialTopPosition;
            }

            public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
            {
                base.OnScrolled(recyclerView, dx, dy);

                if (recyclerView.GetChildAt(0).Top < _initialTopPosition)
                {
                    _appBarLayout.Elevation = _initialElevation;
                }
                else
                {
                    _appBarLayout.Elevation = 0;
                }
            }
        }
    }
}
