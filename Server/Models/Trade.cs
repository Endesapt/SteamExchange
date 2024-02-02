using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Index(nameof(UserId))]
    [Index(nameof(TradeUpTime))]
    public class Trade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public required long UserId { get; set; }
        public required string TradeJSON {  get; set; }
        
        public required DateTime? TradeTime { get; set; }
        public required DateTime? TradeUpTime { get; set; }

        public int Likes { get; set; } = 0;
        public int OffersCount { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;

        public List<TradeOffer> Offers { get; set; } = new();
        public List<TradeComment> Comments { get; set; } = new();
    }
}
