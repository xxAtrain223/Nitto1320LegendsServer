using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nitto1320LegendsServer.Models;

namespace Nitto1320LegendsServer.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            AppUser user = new AppUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return BadRequest(String.Join("; ", result.Errors.Select(e => e.Description).ToList()));
            }

            return Ok();
        }
    }
}