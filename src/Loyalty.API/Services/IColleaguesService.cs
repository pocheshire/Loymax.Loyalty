using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.API.Models;

namespace Loyalty.API.Services
{
    public interface IColleaguesService
    {
        Task<IEnumerable<Colleague>> GetColleagues();
    }
}
