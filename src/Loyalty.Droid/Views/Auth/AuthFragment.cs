using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Loyalty.Core.ViewModels.Auth;
using Loyalty.Core.ViewModels.Main;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.Auth
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_modals_frame, false)]
    public class AuthFragment : MvxFragment<AuthViewModel>
    {
        public AuthFragment()
        {
        }

        protected AuthFragment(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_auth, null);

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Window.SetSoftInputMode(SoftInput.AdjustResize);
        }
    }
}
