using Android.OS;
using Android.Views;
using Loyalty.Core.ViewModels.Auth;
using Loyalty.Core.ViewModels.Main;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Loyalty.Droid.Views.Auth
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_modals_frame, false)]
    public class AuthFragment : MvxFragment<AuthViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_auth, null);

            return view;
        }
    }
}
