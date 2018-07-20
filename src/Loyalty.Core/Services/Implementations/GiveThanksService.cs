using System;
using System.Globalization;
using System.Threading.Tasks;
using Loyalty.API.Models;
using Loyalty.API.Services;
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

        public async Task GiveThanks(Colleague colleague)
        {
            var user = SessionService.GetUser();

            try
            {
                var sum = await UserDialog.ShowDecimalPrompt(
                    $"{colleague.Surname} {colleague.Name}",
                    $"Ваш баланс {user.Balance.ToString("C0", CultureInfo.CurrentUICulture.NumberFormat)}",
                    user.Balance > 100 ? "100" : user.Balance.ToString("##", CultureInfo.CurrentUICulture.NumberFormat)
                );

                var result = false;
                if (user.Balance - sum >= 0)
                {
                    try
                    {
                        result = await ColleaguesService.GiveThanks(colleague.Id, sum);
                    }
                    catch (Exception ex)
                    {
                        Mvx.Resolve<IMvxLog>().ErrorException(ex.Message, ex);
                    }

                    if (result)
                    {
                        user.Balance -= sum;

                        UserDialog.ShowAlert("Коллега скажет вам спасибо!");
                    }
                    else
                        UserDialog.ShowAlert("Не удалось поблагодарить коллегу, попробуйте позже");
                }
                else
                    UserDialog.ShowAlert(user.Balance > 0 ? "Введите меньшую сумму бонусов" : "Вы уже перечислили все доступные вам бонусы");
            }
            catch (OperationCanceledException)
            {

            }
        }
    }
}
