using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Server.Controllers
{
    public class HealthController : Controller
    {
        [HttpGet("/")]
        public string Index()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
