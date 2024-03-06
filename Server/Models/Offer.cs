using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Index(nameof(PostTime))]
    [Index(nameof(UserId))]
    [Index(nameof(UpTime))]
    public class Offer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public User User { get; set; } = null!;
        public required long UserId {  get; set; }

        public required DateTime PostTime { get; set; }
        public required DateTime UpTime { get; set; }
    }
}
