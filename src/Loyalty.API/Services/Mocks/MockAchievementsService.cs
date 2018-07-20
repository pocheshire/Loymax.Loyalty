using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.API.Models;

namespace Loyalty.API.Services.Mocks
{
    public class MockAchievementsService : IAchievementsService
    {
        private readonly List<Achievement> _achievements;

        public MockAchievementsService()
        {
            _achievements = new List<Achievement>
            {
                new Achievement { Id = "000", ImageUrl = "https://image.ibb.co/fXsKeJ/first_achievement.png" }
            };
        }

        public async Task<IEnumerable<Achievement>> GetAchievements(string userId)
        {
            await Task.Delay(300);

            return _achievements;
        }
    }
}
