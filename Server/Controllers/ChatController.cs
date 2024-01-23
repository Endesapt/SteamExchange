using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Helpers;
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
        private readonly ApplicationDbContext _context;
        private readonly IChatService _chatService;
        public ChatController(ApplicationDbContext dbContext,IChatService chatService) {
            _context = dbContext;
            _chatService = chatService;
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

            long toUserId = chat.UserId1 == userId ? chat.UserId2 : chat.UserId1;
            Message message = await _chatService.PostMessage(messageRequest, userId, toUserId);

            return message;
        }
        [HttpGet("chat")]
        public async Task<ActionResult<GetMessagesResponce>> GetMessages(int chatId,long fromId=-1,int limit=20)
        {
            if (!ModelState.IsValid) return BadRequest();
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ExtractIdHelper.ExtractedIdFromClaim(userIdString, out var userId))
                return BadRequest("Cant extract steamID from AuthClaim");
            if (!_chatService.CanUseChat(chatId, userId,out _)) return BadRequest("You cannot access this chat");

            IEnumerable<Message> messages=_chatService.GetMessages(chatId, fromId,limit);
            GetMessagesResponce responce = new()
            {
                LastMessageId = messages.LastOrDefault()?.Id ?? -1,
                Messages = messages
            };
            return Ok(responce);


        }
    }
}
