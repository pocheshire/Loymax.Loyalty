using System.Threading.Tasks;
using Android.Support.V7.App;
using Loyalty.Core.Services;
using MvvmCross;
using MvvmCross.Platforms.Android;
using Android.Support.Design.Widget;
using System;

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

        public Task<decimal> ShowDecimalPrompt(string title, string message, string text)
        {
            var tcs = new TaskCompletionSource<decimal>();

            var currentActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

            var alertDialog = new AlertDialog.Builder(currentActivity)
                                   .SetTitle(title)
                                   .SetMessage(message)
                                   .SetPositiveButton("ОК", (sender, e) =>
                                    {
                                        var dialog = sender as AlertDialog;
                                        var editText = dialog.FindViewById<TextInputEditText>(Resource.Id.editText);

                                        if (!string.IsNullOrEmpty(editText.Text) && !string.IsNullOrWhiteSpace(editText.Text))
                                            tcs.TrySetResult(Decimal.Parse(editText.Text));
                                        else
                                            tcs.TrySetException(new OperationCanceledException());
                                    })
                                   .SetNegativeButton("ОТМЕНА", (sender, e) => { tcs.TrySetException(new OperationCanceledException()); })
                                   .SetView(Resource.Layout.include_userdialog_edittext)
                                   .Show();

            alertDialog.FindViewById<TextInputEditText>(Resource.Id.editText).Text = text;

            return tcs.Task;
        }
    }
}
