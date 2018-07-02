using Loyalty.Core.ViewModels.Colleagues;
using Loyalty.Core.ViewModels.Main;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.Colleagues
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content, false)]
    public class ColleaguesFragment : MvxFragment<ColleaguesViewModel>
    {
        public ColleaguesFragment()
        {
        }
    }
}
