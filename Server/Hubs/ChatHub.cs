using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Server.Models;

namespace Server.Hubs
{
    [Authorize]
    public class ChatHub:Hub
    {
    }
}
