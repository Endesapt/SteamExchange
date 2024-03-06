using Newtonsoft.Json;
using Server.Models;
using System.Text.Json.Serialization;

namespace Server.ResponseModels
{
    public class WeaponsInfo
    {
        [JsonPropertyName("items_list")]
        public Dictionary<string,Weapon> ItemsList { get; set; }
    }
}
