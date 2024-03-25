using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services.Interfaces
{
    public interface IRequestService
    {
        public Task<T> GetAsync<T>(string url, int minutes = 15, bool forceRefresh = false);
        public Task<bool> PostAsync(string url,object obj);
        public Task<bool> CreateOfferAsync(IEnumerable<UserWeapon> weapons);

    }
}
