using Client.Services.Interfaces;
using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    
    public class AuthorizationHandlerService:IAuthorizationHandler
    {
        private readonly OidcClient _authClient;

        public AuthorizationHandlerService(OidcClient authClient)
        {
            _authClient = authClient;
        }

        public string? AuthToken { get; set;}

        public async Task<string?> LoginAsync()
        {
            var loginResult = await _authClient.LoginAsync(new LoginRequest());
            if (loginResult.IsError)
                return null;

            await SecureStorage.Default.SetAsync("access_token", loginResult.AccessToken);
            if (loginResult.RefreshToken != null)
            {
                await SecureStorage.Default.SetAsync("refresh_token", loginResult.RefreshToken);
            }
            AuthToken= loginResult.AccessToken;
            return AuthToken;
        }
        public async Task<string?> GetAccessTokenAsync()
        {
            if (AuthToken is not null) return AuthToken;
            AuthToken = await SecureStorage.Default.GetAsync("access_token");

            return AuthToken;
        }

    }
}
