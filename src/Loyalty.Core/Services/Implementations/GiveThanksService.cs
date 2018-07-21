using System;
using System.Globalization;
using System.Threading.Tasks;
using Loyalty.API.Models;
using Loyalty.API.Services;
using Loyalty.Core.Models;
using MvvmCross;
using MvvmCross.Logging;

namespace Loyalty.Core.Services.Implementations
{
    public class GiveThanksService : IGiveThanksService
    {
        ISessionService SessionService { get; }

        IUserDialog UserDialog { get; }

        IColleaguesService ColleaguesService { get; }

        public GiveThanksService(IUserDialog userDialog, IColleaguesService colleaguesService)
        {
            SessionService = Mvx.Resolve<ISessionService>();

            UserDialog = userDialog;
            ColleaguesService = colleaguesService;
        }

        public async Task<GiveThanksResult> GiveThanks(decimal? sum, string comment, Colleague colleague)
        {
            var operationResult = new GiveThanksResult();

            var user = SessionService.GetUser();

            if (!sum.HasValue)
                operationResult.SumResult = new ValidationResult { IsError = true, Error = $"Сколько отправить? Ваш баланс {user.Balance.ToString("C0", new CultureInfo("ru-RU").NumberFormat)}" };
            else if (user.Balance - sum.Value < 0)
                operationResult.SumResult = new ValidationResult { IsError = true, Error = $"Введите меньшую сумму, ваш баланс {user.Balance.ToString("C0", new CultureInfo("ru-RU").NumberFormat)}" };

            if (string.IsNullOrEmpty(comment) || string.IsNullOrWhiteSpace(comment))
                operationResult.CommentResult = new ValidationResult { IsError = true, Error = "Обязательно добавьте комментарий" };

            if (operationResult.SumResult == null && operationResult.CommentResult == null)
            {
                var result = false;
                try
                {
                    result = await ColleaguesService.GiveThanks(colleague.Id, sum.Value, comment);
                }
                catch (Exception ex)
                {
                    Mvx.Resolve<IMvxLog>().ErrorException(ex.Message, ex);
                }

                if (result)
                {
                    user.Balance -= sum.Value;
                    operationResult.Success = true;
                }
                else
                    UserDialog.ShowAlert("К сожалению, благодарочка затерялась в пути.\nПожалуйста, попробуйте еще раз");
            }

            return operationResult;
        }
    }
}
