using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.API.Models;

namespace Loyalty.API.Services.Mocks
{
    public class MockColleaguesService : IColleaguesService
    {
        public MockColleaguesService()
        {
        }

        public Task<IEnumerable<Colleague>> GetColleagues()
        {
            throw new NotImplementedException();
        }
    }
}
