using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Loyalty.Core.ViewModels.Main;
using Loyalty.Core.ViewModels.Profile;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.Profile
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false, Resource.Animation.abc_popup_enter, Resource.Animation.abc_popup_exit)]
    public class ProfileFragment : MvxFragment<ProfileViewModel>
    {
        public ProfileFragment()
        {
        }

        protected ProfileFragment(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_profile, null);

            return view;
        }
    }
}
