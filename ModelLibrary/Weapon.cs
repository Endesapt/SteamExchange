using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json.Serialization;

namespace ModelLibrary

{
   
    public class Weapon
    {
        [Key]
        public required long ClassId { get; set; }
        public required bool IsTradable { get; set; }
        public required string IconUrl { get; set; } = null!;
        public required string? Type { get; set; } = null!;
        public required string Name { get; set; } = null!;
        public required double Price {  get; set; }

    }

}
