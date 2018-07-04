using Android.Support.V7.App;
using Loyalty.Core.Services;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace Loyalty.Droid.Services.Implementations
{
    public class UserDialog : IUserDialog
    {
        public void ShowAlert(string message)
        {
            var currentActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

            new AlertDialog.Builder(currentActivity)
                                   .SetMessage(message)
                                   .SetPositiveButton("ОК", (sender, e) => { })
                                   .Show();
        }
    }
}
