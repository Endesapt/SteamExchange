using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    public class TestAuthprizeController : Controller
    {
        [HttpGet("/checkAuth")]
        public string Index()
        {
            return User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
