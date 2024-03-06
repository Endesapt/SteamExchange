using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Server.Controllers
{
    public class HealthController : Controller
    {
        [HttpGet("/health")]
        public string Index()
        {
            return "Healthy";
        }
    }
}
