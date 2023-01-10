using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(AppUser appUser)
        {

            if (appUser == null || string.IsNullOrEmpty(appUser.PasswordHash) || string.IsNullOrEmpty(appUser.UserName))
            {
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(appUser.UserName, appUser.PasswordHash, false, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(appUser.UserName);
                user.IsActive = true;
                if (user.AppUserId == 0)
                {
                    user.AppUserId = user.Id;
                }
                var x = await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "User");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View();
        }


        [HttpGet]
        public IActionResult PasswordChange()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //kullanıcı cıkıs yaotıgında aktiflik durumunu 0 yapıyorum ki chatte offline gözüksün
            //await unutma
            user.IsActive = false;
            var x = await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Home");
        }

    }
}


