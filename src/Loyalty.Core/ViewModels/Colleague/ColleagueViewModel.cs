using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.Colleague
{
    public class ColleagueViewModel : MvxViewModel<API.Models.Colleague>
    {
        #region Fields

        #endregion

        #region Commands

        #endregion

        #region Properties

        API.Models.Colleague Model { get; set; }

        #endregion

        #region Services

        #endregion

        #region Constructor

        public ColleagueViewModel()
        {

        }

        #endregion

        #region Private

        #endregion

        #region Protected

        protected async Task LoadContent()
        {
            
        }

        #endregion

        #region Public

        public override Task Initialize()
        {
            return LoadContent();
        }

        public override void Prepare(API.Models.Colleague parameter)
        {
            Model = parameter;
        }

        #endregion
    }
}
