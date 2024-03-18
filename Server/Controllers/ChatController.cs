using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using Server.Data;
using Server.Helpers;
using Server.Hubs;
using Server.Models;
using Server.ResponseModels;
using Server.Services;
using Server.Services.Interfaces;
using System.Security.Claims;


namespace Server.Controllers
{
    
    
    [ApiController]
    [Route("api")]
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatController(IChatService chatService,IHubContext<ChatHub> hubContext) {

            _chatService = chatService;
            _hubContext = hubContext;
        }
        [HttpPost("createChat")]
        public async Task<ActionResult<Chat>> CreateChat(long toUserId)
        {
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ExtractIdHelper.ExtractedIdFromClaim(userIdString, out var userId))
                return BadRequest("Cant extract steamID from AuthClaim");
            Chat chat = await _chatService.CreateChat(userId, toUserId);
            return chat;
            
        }
        [HttpPost("postMessage")]
        public async Task<ActionResult<Message>> PostMessage([FromForm] MessageRequest messageRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ExtractIdHelper.ExtractedIdFromClaim(userIdString, out var userId))
                return BadRequest("Cant extract steamID from AuthClaim");

            if (!_chatService.CanUseChat(messageRequest.ChatId, userId, out var chat)) return BadRequest("You cannot access this chat");
            if (messageRequest.Image!=null&&!messageRequest.Image.ContentType.StartsWith("image/")) return BadRequest("content file is not image");

            long toUserId = chat.UserId1 == userId ? chat.UserId2 : chat.UserId1;
            Message message = await _chatService.PostMessage(messageRequest, userId, toUserId);

            await _hubContext.Clients.Users(userId.ToString(),toUserId.ToString()).SendAsync("Receive", message);
            return message;
        }
        [HttpGet("chat")]
        public ActionResult<GetMessagesResponse> GetMessages(int chatId, long fromId = -1, int limit = 20)
        {
            if (!ModelState.IsValid) return BadRequest();
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ExtractIdHelper.ExtractedIdFromClaim(userIdString, out var userId))
                return BadRequest("Cant extract steamID from AuthClaim");
            if (!_chatService.CanUseChat(chatId, userId, out _)) return BadRequest("You cannot access this chat");

            IEnumerable<Message> messages = _chatService.GetMessages(chatId, fromId, limit);
            GetMessagesResponse response = new()
            {
                LastMessageId = messages.LastOrDefault()?.Id ?? -1,
                Messages = messages
            };
            return Ok(response);


        }
    }

}
