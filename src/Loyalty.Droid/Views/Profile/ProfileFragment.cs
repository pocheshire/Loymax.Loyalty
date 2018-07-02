using Loyalty.Core.ViewModels.Main;
using Loyalty.Core.ViewModels.Profile;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.Profile
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content, false)]
    public class ProfileFragment : MvxFragment<ProfileViewModel>
    {
        public ProfileFragment()
        {
        }
    }
}
