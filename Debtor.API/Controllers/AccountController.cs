using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Debtor.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        [HttpPut]
        public async Task Login(string returnURL = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties()
            {
                RedirectUri = returnURL
            });
        }

        [HttpPut]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties(){
                RedirectUri = "/hhh"
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpGet]
        public async Task<string> Profile()
        {
            //var User = View(new
            //{
            //    Name = User.Identity.Name,
            //    EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
            //    ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            //});
            return "you are authorized";
        }
    }
}

