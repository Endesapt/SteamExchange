using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary;
using Server.Models;
using Server.ResponseModels;
using Server.Services.Interfaces;

namespace Server.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly IInventoryService _inventoryService;
        public InventoriesController(IInventoryService inventoryService) {
            _inventoryService = inventoryService;
        }
        [HttpGet("/getInventories")]
        public IEnumerable<User> GetInventories(DateTime? fromTime,int count=20)
        {
            return _inventoryService.GetInventories(fromTime??DateTime.UtcNow, count);
        }
        [HttpGet("/getOffers")]
        public IEnumerable<Offer> GetOffers(DateTime? fromTime, int count = 20)
        {
            return _inventoryService.GetOffers(fromTime ?? DateTime.UtcNow, count);
        }

    }
}
