using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services.Interfaces
{
    public interface IAuthorizationHandler
    {
        public IEnumerable<Claim> UserClaims { get; set; }
        public Task<string?> LoginAsync();
        public Task<string?> GetAccessTokenAsync();
        public Task<bool> IsAuthenticatedAsync();
        public long? GetUserId();
    }
}
