

namespace Client.Models
{
    public class Weapon
    {
        public required long AssetId { get; set; }
        public required long ClassId { get; set; }
        public required string IconUrl { get; set; } = null!;
        public required string Name { get; set; } = null!;
        public required int Count { get; set; }
        public bool IsTradeable { get; set; }
        public required double Price { get; set; }
    }
}
