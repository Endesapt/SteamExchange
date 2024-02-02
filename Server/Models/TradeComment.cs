using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class TradeComment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Trade Trade { get; set; }
        [ForeignKey("Trade")]
        public required Guid TradeId { get; set; }
        public User? User { get; set; }
        public required long UserId {  get; set; }
        public required DateTime CreateTime { get; set; }
        public required string CommentText {  get; set; }


    }
}
