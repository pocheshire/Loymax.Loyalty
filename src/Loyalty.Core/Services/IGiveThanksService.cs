using System.Threading.Tasks;
using Loyalty.API.Models;
using Loyalty.Core.Models;

namespace Loyalty.Core.Services
{
    public interface IGiveThanksService
    {
        Task<GiveThanksResult> GiveThanks(decimal sum, string comment, Colleague colleague);
    }
}
