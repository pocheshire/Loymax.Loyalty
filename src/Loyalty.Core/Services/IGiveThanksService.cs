using System;
using System.Threading.Tasks;
using Loyalty.API.Models;

namespace Loyalty.Core.Services
{
    public interface IGiveThanksService
    {
        Task GiveThanks(Colleague colleague);
    }
}
