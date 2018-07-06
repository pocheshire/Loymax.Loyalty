using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Loyalty.Core.ViewModels.Colleagues;
using Loyalty.Core.ViewModels.Main;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.Colleagues
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
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
    }
}
