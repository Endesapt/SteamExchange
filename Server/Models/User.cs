using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Models
{
    public class User
    {
        [Key]
        public required long SteamId { get; set; }
        public required string UserName { get; set; }
        public required string AvatarHash { get; set; }

    }
}
