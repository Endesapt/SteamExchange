using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services.Interfaces
{
    public interface IRequestService
    {
        public Task<T> GetAsync<T>(string url, int hours = 8, bool forceRefresh = false);
    }
}
