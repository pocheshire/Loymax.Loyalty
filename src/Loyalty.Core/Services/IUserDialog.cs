using System;
using System.Threading.Tasks;

namespace Loyalty.Core.Services
{
    public interface IUserDialog
    {
        void ShowAlert(string message);

        Task<decimal> ShowDecimalPrompt(string title, string message, string text);
    }
}
