using System.Collections.Generic;
using System.Reflection;
using Loyalty.Core.Services;
using Loyalty.Droid.Services.Implementations;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace Loyalty.Droid
{
    public class Setup : MvxAppCompatSetup<Core.App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies =>
            new List<Assembly>(base.AndroidViewAssemblies)
            {
                typeof(MvxRecyclerView).Assembly
            };

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<IUserDialog>(() => new UserDialog());
        }
    }
}
