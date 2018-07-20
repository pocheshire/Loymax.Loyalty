using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Loyalty.API.Models;

namespace Loyalty.API.Services
{
    public interface IAchievementsService
    {
        Task<IEnumerable<Achievement>> GetAchievements(string userId);
    }
}
