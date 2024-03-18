using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary;
using Server.Helpers;
using Server.Models;
using Server.ResponseModels;
using Server.Services.Interfaces;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("/checkUserCreated")]
        public async Task<ActionResult> CheckUserCreated()
        {
            if(!ExtractIdHelper.ExtractedIdFromClaim(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value,out var userId))
            {
                return BadRequest("Cannot extract Id from User");
            }
            await _userService.EnsureUserCreated(userId);
            return Ok();
        } 
        [HttpGet("getProfile")]
        public async Task<ActionResult<User>> GetProfile(long userId)
        {
            User? pm=_userService.GetProfile(userId);
            if (pm == null) return BadRequest($"There is no user with Id {userId}");
            return Ok(pm);
        }
    }
}
