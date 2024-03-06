using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json.Serialization;

namespace Server.Models

{
    public class Price{
        [JsonPropertyName("average")]
        public double? Average { get; set; }
    }
    public class Weapon
    {
        [JsonPropertyName("classid")]
        [Key]
        public long? ClassId { get; set; }
        [JsonPropertyName("tradable")]
        public byte? IsTradable { get; set; }
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; } = null!;
        [JsonPropertyName("type")]
        public string? Type { get; set; } = null!;
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonIgnore]
        public double Price {  get; set; }
        [NotMapped]
        [JsonPropertyName("price")]
        public Dictionary<string, Price> Prices { get; set; } = null!;

    }

}
