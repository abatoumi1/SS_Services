using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SportServiceApp.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;


        public AccountController(UserManager<IdentityUser> userMgr,
                               SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="creds"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel creds,
                string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (await DoLogin(creds))
                {
                    return Redirect(returnUrl ?? "/");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View(creds);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string redirectUrl)
        {
            await signInManager.SignOutAsync();
            return Redirect(redirectUrl ?? "/");
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="creds"></param>
        /// <returns></returns>
        [HttpPost("/api/account/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel creds)
        {
            if (ModelState.IsValid && await DoLogin(creds))
            {
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// Log Out
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        private async Task<bool> DoLogin(LoginViewModel creds)
        {
            IdentityUser user = await userManager.FindByNameAsync(creds.Name);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(user, creds.Password,
                        false, false);
                return result.Succeeded;
            }
            return false;
        }
    }

    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

