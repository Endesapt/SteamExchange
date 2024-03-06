using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class UserWeapon
    {
        public required long UserId {  get; set; }
        public User User { get; set; } = null!;
        public required long WeaponClassId {  get; set; }
        public Weapon Weapon { get; set; } = null!;
        public required int Count { get; set; }
        public bool IsTradeable { get; set; }
    }
}
