using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.API.Models;

namespace Loyalty.API.Services
{
    public interface IColleaguesService
    {
        Task<IEnumerable<Colleague>> GetColleagues();

        Task<IEnumerable<Colleague>> SearchColleagues(string query);

        Task<bool> GiveThanks(string id, decimal sum, string comment);
    }
}
