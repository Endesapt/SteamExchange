using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using Server.Data;
using Server.Models;
using Server.Services.Interfaces;
using System.Text;
using static AspNet.Security.OpenId.OpenIdAuthenticationConstants;

namespace Server.Services
{
    public class ChatService:IChatService
    {
        public readonly ApplicationDbContext _context;
        public readonly IConfiguration _configuration;
        public readonly IAmazonS3 _s3Client;
        public ChatService(ApplicationDbContext context, IConfiguration configuration,IAmazonS3 s3Client)
        {
            _context = context;
            _configuration = configuration;
            _s3Client = s3Client;
        }
        public async Task<Chat> CreateChat(long userId,long toUserId)
        {
            Chat chat = await _context.Chats.FirstOrDefaultAsync(c =>
            (c.UserId1 == userId && c.UserId2 == toUserId) ||
            (c.UserId1 == toUserId && c.UserId2 == userId)
            );
            if (chat != null) return chat;
            chat = new()
            {
                UserId1 = userId,
                UserId2 = toUserId,
                IsBanned = false,
            };
            await _context.AddAsync(chat);
            await _context.SaveChangesAsync();
            return chat;
        }
        public async Task<Message> PostMessage(MessageRequest model,long userId,long toUserId)
        {
            
            Message message = new() {
                ChatId = model.ChatId,
                FromUserId = userId,
                ToUserId=toUserId,
                SendTime = DateTime.UtcNow,
                MessageText = model.MessageText,
                
            };

            if (model.Image != null) {
                string keyString = $"{model.ChatId}/{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
                var request = new PutObjectRequest()
                {
                    BucketName = _configuration["S3BucketName"],
                    Key =keyString,
                    InputStream =model.Image.OpenReadStream()
                };
                request.Metadata.Add("Content-Type", model.Image.ContentType);
                await _s3Client.PutObjectAsync(request);
                message.ImageKey = keyString;
            }
            await _context.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }
        

        public IEnumerable<Message>? GetMessages(int chatId, long fromId, int limit)
        {
            var messages = _context.Messages.Where((m) => m.ChatId == chatId);
            if (fromId > 0)
                return messages.Where(m => m.Id < fromId).OrderByDescending(m => m.Id).Take(limit);
            return messages.OrderByDescending(m => m.Id).Take(limit);


        }

        public bool CanUseChat(int chatId, long userId, out Chat chat)
        {
            chat = _context.Chats.Find(chatId);
            if (chat == null) return false;
            if (chat.UserId1 != userId && chat.UserId2 != userId) return false;
            if (chat.IsBanned) return false;
            return true;
        }
    }
}
