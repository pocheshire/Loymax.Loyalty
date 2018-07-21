using System;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Loyalty.Core.ViewModels.GiveThanks;
using Loyalty.Core.ViewModels.Main;
using Loyalty.Droid.Views.Main;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Animation;

namespace Loyalty.Droid.Views.GiveThanks
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_modals_frame, true, Resource.Animation.abc_slide_in_bottom, Resource.Animation.abc_slide_out_bottom, Resource.Animation.abc_slide_in_bottom, Resource.Animation.abc_slide_out_bottom)]
    public class GiveThanksFragment : MvxFragment<GiveThanksViewModel>
    {
        public GiveThanksFragment()
        {
        }

        protected GiveThanksFragment(IntPtr javaReference, JniHandleOwnership transfer) 
            : base(javaReference, transfer)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_give_thanks, null);

            var t = view.FindViewById<TextInputLayout>(Resource.Id.action0);

            return view;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var valueAnimator = ValueAnimator.OfFloat(0, 1);
            valueAnimator.Update += (object sender, ValueAnimator.AnimatorUpdateEventArgs e) => 
            {
                (Activity as MainActivity).ContentModalsShadow.Alpha = (float)e.Animation.AnimatedValue;
            };
            valueAnimator.SetDuration(300);
            valueAnimator.Start();
        }

        public override void OnDestroyView()
        {
            var valueAnimator = ValueAnimator.OfFloat(1, 0);
            valueAnimator.Update += (object sender, ValueAnimator.AnimatorUpdateEventArgs e) =>
            {
                (Activity as MainActivity).ContentModalsShadow.Alpha = (float)e.Animation.AnimatedValue;
            };
            valueAnimator.SetDuration(300);
            valueAnimator.Start();

            base.OnDestroyView();
        }
    }
}
