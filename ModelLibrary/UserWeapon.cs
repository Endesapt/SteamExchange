using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelLibrary
{
    public class UserWeapon
    {
        public required long UserId {  get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;
        [JsonPropertyName("classid")]
        public required long WeaponClassId {  get; set; }
        public required long AssetId { get; set; }
        public Weapon Weapon { get; set; } = null!;
        [JsonPropertyName("amount")]
        public required int Count { get; set; }
        public bool IsInAuction {  get; set; }
    }
}
