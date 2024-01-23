using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Services.Interfaces;
using System.Text;

namespace Server.Services
{
    public enum ImageFormat
    {
        bmp,
        gif,
        png,
        jpeg,
        unknown=-1
    }
    public class ChatService:IChatService
    {
        public readonly ApplicationDbContext _context;
        public ChatService(ApplicationDbContext context)
        {
            _context = context;
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

            if (model.Image != null) message.ImageURl = model.Image.Name;
            await _context.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }
        //function that i lately maybe will use to push images to db
        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            // see http://www.mikekunz.com/image_file_header.html  
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
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
