using Android.Support.V7.App;
using Loyalty.Core.Services;

namespace Loyalty.Droid.Services.Implementations
{
    public class UserDialog : IUserDialog
    {
        public void ShowAlert(string message)
        {
            new AlertDialog.Builder(Android.App.Application.Context)
                                   .SetMessage(message)
                                   .SetPositiveButton("ОК", (sender, e) => { })
                                   .Show();
        }
    }
}
