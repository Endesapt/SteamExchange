using ModelLibrary;
using Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Server.ResponseModels
{
    public class WeaponsInfo
    {
        [JsonPropertyName("items_list")]
        public Dictionary<string,WeaponDto> ItemsList { get; set; }
    }
    public class Price
    {
        [JsonPropertyName("average")]
        public double? Average { get; set; }
    }
    public class WeaponDto
    {
        [JsonPropertyName("classid")]
        public long? ClassId { get; set; }
        [JsonPropertyName("tradable")]
        public byte? IsTradable { get; set; }
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; } = null!;
        [JsonPropertyName("type")]
        public string? Type { get; set; } = null!;
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [System.Text.Json.Serialization.JsonIgnore]
        public double Price { get; set; }
        [NotMapped]
        [JsonPropertyName("price")]
        public Dictionary<string, Price> Prices { get; set; } = null!;
    }
}
