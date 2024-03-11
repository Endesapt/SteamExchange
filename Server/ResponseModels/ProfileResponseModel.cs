using Server.Dto;
using Server.Models;

namespace Server.ResponseModels
{
    public class ProfileResponseModel
    {
        public User User { get; set; }
        public List<WeaponDto> Weapons { get; set; }
    }
}
