using Microsoft.AspNetCore.Mvc;
using ModelLibrary;
using Server.Models;

namespace Server.Services.Interfaces
{
    public interface IChatService
    {
        public Task<Chat> CreateChat(long userId, long toUserId);
        public Task<Message> PostMessage(MessageRequest model, long userId, long toUserId);

        public IEnumerable<Message> GetMessages(int chatId, long fromId,int limit);
        public bool CanUseChat(int chatId, long userId,out Chat chat);
    }
}
