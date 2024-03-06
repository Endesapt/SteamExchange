using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services.Interfaces
{
    public interface IAuthorizationHandler
    {
        public string? AuthToken {  get; set; }
        public Task<string?> LoginAsync();
        public Task<string?> GetAccessTokenAsync();
    }
}
