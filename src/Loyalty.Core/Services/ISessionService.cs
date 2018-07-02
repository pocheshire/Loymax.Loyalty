using System;
using System.Threading.Tasks;
using Loyalty.API.Models;

namespace Loyalty.Core.Services
{
    interface ISessionService
    {
        bool IsSignedIn();

        Task Init(string username, string password);

        Task<bool> StartSession();

        Task StopSession();

        User GetUser();
    }
}
