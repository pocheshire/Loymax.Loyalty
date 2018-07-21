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
                new Colleague { Id = "01", Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "http://blablabla/west_fil_cwsfd.png" },
                new Colleague { Id = "02", Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "" },
                new Colleague { Id = "03", Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "" },
                new Colleague { Id = "04", Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "" },
                new Colleague { Id = "05", Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "" },
                new Colleague { Id = "06", Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "" },
                new Colleague { Id = "07", Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "" },
                new Colleague { Id = "08", Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "" },
                new Colleague { Id = "09", Surname = "Константинопольский", Name = "Константин", MiddleName = "Константинович", RoleName = "Техническая поддержка", ImageUrl = "" },
                new Colleague { Id = "10", Surname = "Александрова", Name = "Александра", MiddleName = "Александровна", RoleName = "Аккаунт-менеджер", ImageUrl = "" }
            };
        }

        public Task<IEnumerable<Colleague>> GetColleagues()
        {
            return Task.FromResult(_colleagues);
        }

        public Task<bool> GiveThanks(string id, decimal sum, string coment)
        {
            return id == _colleagues.ElementAt(4).Id ? Task.FromResult(false) : Task.FromResult(true);
        }

        public async Task<IEnumerable<Colleague>> SearchColleagues(string query)
        {
            await Task.Delay(300);

            query = query.ToLowerInvariant();

            return string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query) ?
                             _colleagues
                                 :
                         _colleagues.Where(x => x.Name.ToLowerInvariant().Contains(query) 
                                           || x.RoleName.ToLowerInvariant().Contains(query) 
                                           || x.Surname.ToLowerInvariant().Contains(query) 
                                           || x.MiddleName.ToLowerInvariant().Contains(query));
        }
    }
}
