using System;
using System.Collections.Generic;
using System.Linq;
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
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "http://blablabla/west_fil_cwsfd.png" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "" },
                new Colleague { Id = Guid.NewGuid().ToString(), Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "" }
            };
        }

        public Task<IEnumerable<Colleague>> GetColleagues()
        {
            return Task.FromResult(_colleagues);
        }

        public Task<bool> GiveThanks(string id, decimal sum)
        {
            return id == _colleagues.ElementAt(4).Id ? Task.FromResult(false) : Task.FromResult(true);
        }
    }
}
