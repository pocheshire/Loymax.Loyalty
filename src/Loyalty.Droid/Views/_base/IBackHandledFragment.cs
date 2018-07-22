using System;
using Android.Runtime;
using Android.Views;

namespace Loyalty.Droid.Views
{
    public interface IBackHandledFragment
    {
        bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e);
    }
}
