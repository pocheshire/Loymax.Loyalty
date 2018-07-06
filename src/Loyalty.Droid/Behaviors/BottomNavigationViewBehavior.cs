using System;
using Android.Content;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;

namespace Loyalty.Droid.Behaviors
{
    public class BottomNavigationViewBehavior : CoordinatorLayout.Behavior
    {
        private ScrollDirection _scrollDirection;

        protected BottomNavigationViewBehavior(IntPtr javaReference, JniHandleOwnership transfer) 
            : base(javaReference, transfer)
        {
        }

        public BottomNavigationViewBehavior()
        {
        }

        public BottomNavigationViewBehavior(Context context, IAttributeSet attrs) 
            : base(context, attrs)
        {
        }

        public override bool OnStartNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View directTargetChild, View target, int axes, int type)
        {
            _scrollDirection = ScrollDirection.UNKNOWN;

            return axes == ViewCompat.ScrollAxisVertical;
        }

        public override void OnNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View target, int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed, int type)
        {
            base.OnNestedScroll(coordinatorLayout, child, target, dxConsumed, dyConsumed, dxUnconsumed, dyUnconsumed, type);

            var view = child as View;
            view.TranslationY = Math.Max(0f, Math.Min(view.Height, view.TranslationY + dyConsumed));

            if (dyConsumed > 0)
                _scrollDirection = ScrollDirection.SCROLL_DOWN;
            else if (dyConsumed < 0)
                _scrollDirection = ScrollDirection.SCROLL_UP;
        }

        public override void OnStopNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View target, int type)
        {
            base.OnStopNestedScroll(coordinatorLayout, child, target, type);

            var view = child as BottomNavigationView;

            if (_scrollDirection == ScrollDirection.SCROLL_DOWN && view.TranslationY < view.Height)
                SlideDown(view);
            else if (_scrollDirection == ScrollDirection.SCROLL_UP && view.TranslationY > 0 && view.TranslationY < view.Height)
                SlideUp(view);

            _scrollDirection = ScrollDirection.UNKNOWN;
        }

        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            if (dependency is Snackbar.SnackbarLayout snackbar)
            {
                UpdateSnackbar(child, snackbar);
                return true;
            }

            return false;
        }

        public override void OnDependentViewRemoved(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            (child as View).TranslationY = 0f;
            base.OnDependentViewRemoved(parent, child, dependency);
        }

        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            UpdateButton(child as View, dependency);
            return base.OnDependentViewChanged(parent, child, dependency);
        }

        private void SlideUp(BottomNavigationView child)
        {
            child.ClearAnimation();
            child.Animate()
                 .TranslationY(0)
                 .SetDuration(200);
        }

        private void SlideDown(BottomNavigationView child)
        {
            child.ClearAnimation();
            child.Animate()
                 .TranslationY(child.MeasuredHeight)
                 .SetDuration(200);
        }

        private void UpdateSnackbar(Java.Lang.Object child, Snackbar.SnackbarLayout snackbarLayout)
        {
            var view = child as View;
            if (snackbarLayout.LayoutParameters is CoordinatorLayout.LayoutParams lp)
            {
                lp.AnchorId = view.Id;
                lp.AnchorGravity = (int)GravityFlags.Top;
                lp.Gravity = (int)GravityFlags.Top;

                snackbarLayout.LayoutParameters = lp;
            }
        }

        private bool UpdateButton(View child, View dependency)
        {
            if (dependency is Snackbar.SnackbarLayout)
            {
                var oldTranslation = child.TranslationY;
                var height = dependency.Height;
                var newTranslation = dependency.TranslationY - height;
                child.TranslationY = newTranslation;

                return System.Math.Abs(oldTranslation - newTranslation) > 0;
            }

            return false;
        }

        public enum ScrollDirection
        {
            UNKNOWN,
            
            SCROLL_UP,

            SCROLL_DOWN
        }
    }
}
