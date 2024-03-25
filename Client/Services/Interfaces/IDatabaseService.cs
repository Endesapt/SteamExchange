using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services.Interfaces
{
    public interface IDatabaseService
    {
        Task<IEnumerable<Message>> GetMessages(int chatId, long fromId = -1, int limit = 20);
    }
}
