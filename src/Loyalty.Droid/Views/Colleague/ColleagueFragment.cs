using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Loyalty.Core.ViewModels.Colleague;
using Loyalty.Core.ViewModels.Main;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.Colleague
{
    [MvxFragmentPresentation(
        typeof(MainViewModel)
        , Resource.Id.content_frame
        , true
        , enterAnimation: Resource.Animation.abc_fade_in
        , exitAnimation: Resource.Animation.abc_fade_out
        , popEnterAnimation: Resource.Animation.abc_fade_in
        , popExitAnimation: Resource.Animation.abc_fade_out
        , isCacheableFragment: true
    )]
    public class ColleagueFragment : MvxFragment<ColleagueViewModel> 
    {
        public ColleagueFragment()
        {
        }

        protected ColleagueFragment(IntPtr javaReference, JniHandleOwnership transfer) 
            : base(javaReference, transfer)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_colleague, null);

            return view;
        }
    }
}
