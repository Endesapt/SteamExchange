using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary;
using Server.Helpers;
using Server.Models;
using Server.ResponseModels;
using Server.Services.Interfaces;
using System.Globalization;
using System.Net.WebSockets;
using System.Security.Claims;

namespace Server.Controllers
{
    public class OffersController : Controller
    {
        private readonly IOfferService _offerService;
        public OffersController(IOfferService inventoryService) {
            _offerService = inventoryService;
        }
        [HttpGet("/getInventories")]
        public IEnumerable<User> GetInventories(string fromTimeString,int count=10)
        {
            if(DateTime.TryParseExact(fromTimeString, "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var fromTime))
            {
                return _offerService.GetInventories(fromTime, count);
            }
            return _offerService.GetInventories(DateTime.UtcNow, count);

        }
        [HttpGet("/getOffers")]
        public ActionResult<IEnumerable<Offer>> GetOffers(string fromTimeString, int count = 20)
        {
            if (DateTime.TryParseExact(fromTimeString, "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var fromTime))
            {
                return Ok(_offerService.GetOffers(fromTime, count));
            }
            return Ok(_offerService.GetOffers(DateTime.UtcNow, count));
        }
        [HttpPost("/createAuction")]
        public async Task<IActionResult> CreateOffer([FromBody]IEnumerable<UserWeapon> userWeapons)
        {
            if (!ExtractIdHelper.ExtractedIdFromClaim(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return BadRequest("Cannot extract Id from User");
            }
            var offer=await _offerService.createOffer(userWeapons, userId);
            if(offer is null) {
                return BadRequest("Not all of the weapons are yours");
            }
            return Ok();

        }

    }
}
