
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Client.Controllers;

public class AuthenticationController : Controller
{

    [HttpGet("~/signin")]
    public IActionResult SignIn()
    {
        return Challenge(new AuthenticationProperties { RedirectUri = "/" },"Steam");
    }

    [HttpGet("~/signout"), HttpPost("~/signout")]
    public IActionResult SignOutCurrentUser()
    {
        return SignOut(new AuthenticationProperties { RedirectUri = "/" },
            CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
