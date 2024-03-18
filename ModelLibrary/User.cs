using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ModelLibrary
{
    [Index(nameof(InventoryUpgradeTime))]
    public class User
    {
        [Key]
        public required long Id { get; set; }
        public required string UserName { get; set; }
        public required string AvatarHash { get; set; }
        public required DateTime InventoryUpgradeTime { get; set; }
        public string? Status {  get; set; }
        public bool HasPremium { get; set; } = false;
        public DateOnly? PremiumDate { get; set; } = null;

        public List<UserWeapon> Weapons { get; set; } = null!;
    }
}
