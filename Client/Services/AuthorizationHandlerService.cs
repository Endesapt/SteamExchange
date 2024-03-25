using Client.Services.Interfaces;
using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    
    public class AuthorizationHandlerService:IAuthorizationHandler
    {
        private readonly OidcClient _client;

        public AuthorizationHandlerService(OidcClient authClient)
        {
            _client = authClient;
        }

        string? AccessToken { get; set;}
        public IEnumerable<Claim> UserClaims { get; set;}

        public async Task<string?> LoginAsync()
        {
            var loginResult = await _client.LoginAsync(new LoginRequest());
            if (loginResult.IsError)
                return null;

            await SecureStorage.Default.SetAsync("access_token", loginResult.AccessToken);
            if (loginResult.RefreshToken != null)
            {
                await SecureStorage.Default.SetAsync("refresh_token", loginResult.RefreshToken);
            }
            AccessToken= loginResult.AccessToken;
            UserClaims=loginResult.User.Claims;
            return AccessToken;
        }
        public async Task<string?> GetAccessTokenAsync()
        {
            if (AccessToken is not null) return AccessToken;
            AccessToken = await SecureStorage.Default.GetAsync("access_token");

            return AccessToken;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
#if DEBUG
            return true;
#endif
            var accessToken = await GetAccessTokenAsync();
            if(accessToken is null) return false;
            var userInfo = await _client.GetUserInfoAsync(accessToken);
            if (userInfo.IsError) return false;
            UserClaims = userInfo.Claims;
            return true;
        }

        public long? GetUserId()
        {
#if DEBUG
            return 76561198970753428;
#endif
            var userIdString = UserClaims?.FirstOrDefault((c) => c.Type == "sub")?.Value;
            if(userIdString == null) return null;
            var splicedUrl = userIdString.Split('/');
            if (long.TryParse(splicedUrl.Last(), out var userId))
            {
                return userId;
            }
            return null;
        }

    }
}
