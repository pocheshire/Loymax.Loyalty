﻿using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Loyalty.Core.ViewModels.GiveThanks;
using Loyalty.Core.ViewModels.Main;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.GiveThanks
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_modals_frame, false, Resource.Animation.abc_slide_in_top, Resource.Animation.abc_slide_in_bottom)]
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

            return view;
        }
    }
}