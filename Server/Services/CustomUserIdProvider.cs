using Microsoft.AspNetCore.SignalR;
using Server.Helpers;
using System.Security.Claims;

namespace Server.Services
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string? GetUserId(HubConnectionContext connection)
        {
            ExtractIdHelper.ExtractedIdFromClaim(connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var userId);
            return userId.ToString();
        }
    }
}
