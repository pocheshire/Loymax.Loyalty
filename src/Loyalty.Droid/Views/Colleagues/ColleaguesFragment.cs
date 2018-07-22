using System;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Loyalty.Core.ViewModels.Colleagues;
using Loyalty.Core.ViewModels.Main;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.Colleagues
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false, Resource.Animation.abc_popup_enter, Resource.Animation.abc_popup_exit, isCacheableFragment: true)]
    public class ColleaguesFragment : MvxFragment<ColleaguesViewModel>, IBackHandledFragment
    {
        private LinearLayout _customToolbar;
        private LinearLayout _searchLayout;
        private Android.Support.V7.Widget.SearchView _searchView;

        bool SearchHidden { get; set; } = true;

        public ColleaguesFragment()
        {
        }

        protected ColleaguesFragment(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        private void HideSearch()
        {
            _customToolbar.Visibility = ViewStates.Visible;
            _searchLayout.Visibility = ViewStates.Gone;

            HideKeyboard();

            SearchHidden = true;

            _searchView.SetQuery(string.Empty, false);

            ViewModel.CancelSearchCommand.Execute();
        }

        private void ShowSearch()
        {
            _customToolbar.Visibility = ViewStates.Gone;
            _searchLayout.Visibility = ViewStates.Visible;

            _searchView.OnActionViewExpanded();
            _searchView.Activated = true;
            _searchView.ClearFocus();

            ShowKeyboard(_searchView);

            SearchHidden = false;
        }

        private void HideKeyboard()
        {
            View.Post(() =>
            {
                var imm = (InputMethodManager)Context?.GetSystemService(Android.Content.Context.InputMethodService);
                if (imm != null && Activity?.Window?.CurrentFocus?.WindowToken != null)
                    imm.HideSoftInputFromWindow(Activity.Window.CurrentFocus.WindowToken, 0);
            });
        }

        private void ShowKeyboard(View view)
        {
            View.Post(() =>
            {
                var imm = (InputMethodManager)Context.GetSystemService(Android.Content.Context.InputMethodService);
                if (imm != null)
                    imm.ToggleSoftInputFromWindow(view.ApplicationWindowToken, ShowSoftInputFlags.Forced, HideSoftInputFlags.None);

                view.RequestFocus();
            });
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

            _customToolbar = appBarLayout.FindViewById<LinearLayout>(Resource.Id.fragment_colleagues_customToolbar);
            _searchLayout = appBarLayout.FindViewById<LinearLayout>(Resource.Id.fragment_colleagues_toolbar_searchLayout);

            _searchView = _searchLayout.FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.fragment_colleagues_toolbar_searchView);
            _searchView.QueryTextSubmit += (sender, e) =>
            {
                ViewModel.SearchCommand.Execute(e.Query);
            };

            var searchButton = appBarLayout.FindViewById<ImageButton>(Resource.Id.fragment_colleagues_toolbar_search);
            searchButton.Click += (sender, e) =>
            {
                searchButton.ClearFocus();

                ShowSearch();
            };

            var closeSearchButton = appBarLayout.FindViewById<ImageButton>(Resource.Id.fragment_colleagues_toolbar_closeSearch);
            closeSearchButton.Click += (sender, e) =>
            {
                HideSearch();
            };

            var swipeRefreshLayout = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.fragment_colleagues_swipeRefreshLayout);
            swipeRefreshLayout.SetColorSchemeResources(Resource.Color.accent, Resource.Color.accent, Resource.Color.accent);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.fragment_colleagues_recyclerView);
            var initialTopPosition = TypedValue.ApplyDimension(ComplexUnitType.Dip, -4, Resources.DisplayMetrics);

            recyclerView.AddOnScrollListener(new OnScrollListener(appBarLayout, initialElevation, (int)initialTopPosition, () => SearchHidden));
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Window.SetSoftInputMode(SoftInput.AdjustPan);
        }

        public bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (!SearchHidden)
            {
                HideSearch();

                return true;
            }

            return false;
        }

        private class OnScrollListener : RecyclerView.OnScrollListener
        {
            private readonly AppBarLayout _appBarLayout;
            private readonly float _initialElevation;
            private readonly int _initialTopPosition;

            public Func<bool> IsElevationEnabled { get; }

            public OnScrollListener(AppBarLayout appBarLayout, float initialElevation, int initialTopPosition, Func<bool> isElevationEnabled)
            {
                _appBarLayout = appBarLayout;
                _initialElevation = initialElevation;
                _initialTopPosition = initialTopPosition;

                IsElevationEnabled = isElevationEnabled;
            }

            public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
            {
                base.OnScrolled(recyclerView, dx, dy);

                if (IsElevationEnabled() && recyclerView.GetChildAt(0)?.Top < _initialTopPosition)
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
