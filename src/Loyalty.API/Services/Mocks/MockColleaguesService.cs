using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.API.Models;

namespace Loyalty.API.Services.Mocks
{
    public class MockColleaguesService : IColleaguesService
    {
        private IEnumerable<Colleague> _colleagues;

        public MockColleaguesService()
        {
            _colleagues = new List<Colleague>
            {
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "https://image.ibb.co/eDOrBd/icons8_320.png" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "https://image.ibb.co/iamBBd/icons8_businesswoman_100.png" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "https://image.ibb.co/eDOrBd/icons8_320.png" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "https://image.ibb.co/iamBBd/icons8_businesswoman_100.png" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "https://image.ibb.co/eDOrBd/icons8_320.png" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "https://image.ibb.co/iamBBd/icons8_businesswoman_100.png" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "https://image.ibb.co/eDOrBd/icons8_320.png" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "https://image.ibb.co/iamBBd/icons8_businesswoman_100.png" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "https://image.ibb.co/eDOrBd/icons8_320.png" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "https://image.ibb.co/iamBBd/icons8_businesswoman_100.png" }
            };
        }

        public Task<IEnumerable<Colleague>> GetColleagues()
        {
            return Task.FromResult(_colleagues);
        }
    }
}
