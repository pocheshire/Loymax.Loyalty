using System;
using System.Threading.Tasks;
using Loyalty.API.Models;

namespace Loyalty.Core.Services
{
    interface ISessionService
    {
        bool IsSignedIn();

        Task<bool> Init(string username, string password);

        Task<bool> StartSession();

        Task<bool> StopSession();

        User GetUser();
    }
}
