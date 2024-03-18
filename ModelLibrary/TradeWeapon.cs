using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLibrary
{
    public class TradeWeapon
    {
        [ForeignKey(nameof(Trade))]
        public required Guid TradeId { get; set; }
        public Trade Trade { get; set; } = null!;
        [ForeignKey("UserId, WeaponClassId,AssetId")]
        public UserWeapon UserWeapon { get; set; } = null!;

        public required long UserId { get; set; }
        public required long WeaponClassId { get; set; }
        public required long AssetId { get; set; }
    }
}
