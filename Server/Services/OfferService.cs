using Microsoft.EntityFrameworkCore;
using ModelLibrary;
using Mysqlx.Crud;
using Server.Data;
using Server.Models;
using Server.ResponseModels;
using Server.Services.Interfaces;
using System.Linq;

namespace Server.Services
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _context;
        public OfferService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Offer?> createOffer(IEnumerable<UserWeapon> userWeapons,long userId)
        {
            var allUserWeapons = _context.UsersWeapons.Where(uw => uw.UserId == userId).ToList();
            foreach(var uw in userWeapons)
            {
                if (allUserWeapons.FirstOrDefault(w=>w.WeaponClassId==uw.WeaponClassId
                && w.Count==uw.Count && uw.AssetId==w.AssetId) is null)
                {
                    return null;
                }
            }
            var offer = new Offer()
            {
                UserId=userId,
                PostTime=DateTime.UtcNow,
                UpTime=DateTime.UtcNow,

            };
            _context.Add(offer);
            var offerWeapons = userWeapons.Select(uw => new OfferWeapon()
            {
                WeaponClassId = uw.WeaponClassId,
                UserId = userId,
                OfferId = offer.Id,
                AssetId=uw.AssetId
            });
            foreach (var ow in offerWeapons)
            {
                _context.Add(ow);
            }
            await _context.SaveChangesAsync();
            return offer;

        }

        public IEnumerable<User> GetInventories(DateTime fromTime, int count = 20)
        {
            var objects = _context.Users
                .Include(u => u.Weapons)
                .ThenInclude(uw=>uw.Weapon)
                .OrderByDescending((u) => u.InventoryUpgradeTime)
                .Where(u=>u.InventoryUpgradeTime<fromTime)
               .Select(u => new User
               {
                   UserName = u.UserName,
                   InventoryUpgradeTime = u.InventoryUpgradeTime,
                   Id = u.Id,
                   AvatarHash = u.AvatarHash,
                   HasPremium = u.HasPremium,
                   Weapons = u.Weapons,
               })
               .Take(count)
               .AsNoTracking().ToList()
               .Select(u => new User
               {
                   UserName = u.UserName,
                   InventoryUpgradeTime = u.InventoryUpgradeTime,
                   Id = u.Id,
                   AvatarHash = u.AvatarHash,
                   HasPremium = u.HasPremium,
                   Weapons = u.Weapons.OrderByDescending(u=>u.Weapon.Price).Take(30).ToList(),
               })
               ;

            return objects;
        }
        public IEnumerable<Offer> GetOffers(DateTime fromTime, int count = 20) {
            var objects = _context.Offers
               .Include(u => u.OfferWeapons)
               .ThenInclude(ow => ow.UserWeapon)
               .ThenInclude(uw=>uw.Weapon)
               .Include(u => u.User)
               .OrderByDescending((u) => u.UpTime)
               .Where(u => u.UpTime < fromTime)
              .Select(o => new Offer
              {
                  UserId = o.UserId,
                  UpTime = o.UpTime,
                  PostTime= o.PostTime,
                  OfferWeapons=o.OfferWeapons,
                  User=o.User,

              })
              .Take(count)
              .AsNoTracking().ToList()
              .Select(o => new Offer
              {
                  User=o.User,
                  UserId = o.UserId,
                  UpTime = o.UpTime,
                  PostTime = o.PostTime,
                  OfferWeapons = o.OfferWeapons.OrderByDescending(ow=>ow.UserWeapon.Weapon.Price).ToList(),
              })
              ;
            return objects;
        }
    }
}
