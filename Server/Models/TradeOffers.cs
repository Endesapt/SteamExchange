using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class TradeOffer
    {
        public required Guid TradeId { get; set; }
        public Trade Trade { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public required DateTime CreateTime { get; set; }
        public required string OfferJSON { get; set; }


    }
}
