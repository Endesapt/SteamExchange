using ModelLibrary;
using Server.Models;

namespace Server.ResponseModels
{
    public class GetMessagesResponse
    {
        public required IEnumerable<Message> Messages { get; set; }
        public required long LastMessageId { get; set; }
    }
}
