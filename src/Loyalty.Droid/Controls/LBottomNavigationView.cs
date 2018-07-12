using System;
using Android.Content;
using Android.Runtime;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;
using Android.Views;

namespace Loyalty.Droid.Controls
{
    [Register("loyalty.droid.controls.LBottomNavigationView")]
    public class LBottomNavigationView : BottomNavigationView
    {
        public LBottomNavigationView(Context context) : base(context)
        {
        }

        public LBottomNavigationView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            CenterMenuIcon();
        }

        public LBottomNavigationView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        protected LBottomNavigationView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        private void CenterMenuIcon()
        {
            BottomNavigationMenuView menuView = GetBottomMenuView();

            if (menuView != null)
            {
                var shiftingMode = menuView.Class.GetDeclaredField("mShiftingMode");
                shiftingMode.Accessible = true;
                shiftingMode.SetBoolean(menuView, false);
                shiftingMode.Accessible = false;

                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    var menuItemView = (BottomNavigationItemView)menuView.GetChildAt(i);

                    for (int j = 0; j < menuItemView.ChildCount; j++)
                    {
                        var view = menuItemView.GetChildAt(i);

                        if (view is ImageView icon)
                        {
                            var layoutParams = (LayoutParams)icon.LayoutParameters;
                            layoutParams.Gravity = Android.Views.GravityFlags.Center;

                            menuItemView.SetShiftingMode(false);
                        }

                        if (view is ViewGroup viewGroup)
                        {
                            menuItemView.RemoveViewAt(i);

                            menuItemView.SetPadding(view.PaddingLeft, (viewGroup.PaddingTop + view.Height) / 2, view.PaddingRight, view.PaddingBottom);
                        }
                    }
                }
            }
        }

        private BottomNavigationMenuView GetBottomMenuView()
        {
            return (BottomNavigationMenuView)this.GetChildAt(0);
        }
    }
}