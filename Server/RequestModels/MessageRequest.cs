namespace Server.Models
{
    public class MessageRequest
    {
        public IFormFile? Image { get; set; }
        public required string MessageText { get; set; }
        public int ChatId { get; set; }
    }
}
