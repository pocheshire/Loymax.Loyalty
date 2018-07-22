using System;
using System.Threading.Tasks;
using Loyalty.API.Models;
using Loyalty.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.GiveThanks
{
    public class GiveThanksViewModel : MvxViewModel<Colleague>
    {
        #region Fields

        #endregion

        #region Commands

        private IMvxCommand _sendCommand;
        public IMvxCommand SendCommand => _sendCommand ?? (_sendCommand = new MvxAsyncCommand(OnSendExecute));

        #endregion

        #region Properties

        Colleague Model { get; set; }

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value, nameof(Loading));
        }

        private TextFieldFrame _sumTextField;
        public TextFieldFrame SumTextField
        {
            get => _sumTextField;
            set => SetProperty(ref _sumTextField, value, nameof(SumTextField));
        }

        private TextFieldFrame _commentTextField;
        public TextFieldFrame CommentTextField
        {
            get => _commentTextField;
            set => SetProperty(ref _commentTextField, value, nameof(CommentTextField));
        }

        #endregion

        #region Services

        ISessionService SessionService { get; }

        IUserDialog UserDialog { get; }

        IGiveThanksService GiveThanksService { get; }

        #endregion

        #region Constructor

        public GiveThanksViewModel(IUserDialog userDialog, IGiveThanksService giveThanksService)
        {
            SessionService = Mvx.Resolve<ISessionService>();

            UserDialog = userDialog;
            GiveThanksService = giveThanksService;

            SumTextField = new TextFieldFrame();
            CommentTextField = new TextFieldFrame();
        }

        #endregion

        #region Private

        private async Task OnSendExecute()
        {
            Loading = true;

            decimal? sum = null;
            if (!string.IsNullOrEmpty(SumTextField.Text) && !string.IsNullOrWhiteSpace(SumTextField.Text))
                sum = Decimal.Parse(SumTextField.Text);

            var comment = CommentTextField.Text;

            var result = await GiveThanksService.GiveThanks(sum, comment, Model);

            Loading = false;

            if (result.Success)
                await NavigationService.Close(this);
            else
            {
                SumTextField.SetValidationResult(result.SumResult);
                CommentTextField.SetValidationResult(result.CommentResult);
            }
        }

        #endregion

        #region Public

        public override void Prepare(Colleague parameter)
        {
            Model = parameter;

            var user = SessionService.GetUser();

            SumTextField.Text = user.Balance > 100 ? "100" : user.Balance.ToString("##");
        }

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
