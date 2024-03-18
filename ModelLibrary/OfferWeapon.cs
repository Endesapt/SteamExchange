using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class OfferWeapon
    {
        public required Guid OfferId { get; set; }
        public Offer Offer { get; set; } = null!;
        [ForeignKey("UserId, WeaponClassId,AssetId")]
        public UserWeapon UserWeapon { get; set; } = null!;

        public required long UserId { get; set; }
        public required long WeaponClassId { get; set; }
        public required long AssetId { get; set; }
    }
}
