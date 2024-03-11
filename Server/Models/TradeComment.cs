using Amazon.S3.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Microsoft.EntityFrameworkCore.Index(nameof(TradeId))]
    public class TradeComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public required long UserId {  get; set; }
        public User User { get; set; } = null!;
        public required Guid TradeId {  get; set; }
        public Trade Trade { get; set; } = null!;
        public required DateTime PostTime { get; set; }
        public required string MessageText { get; set; }

    }
}
