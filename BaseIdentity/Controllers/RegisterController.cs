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
    public class RegisterController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        //Register
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        //Register
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel userSignUp)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = userSignUp.Username,
                    Name = userSignUp.Name,
                    Surname = userSignUp.Surname,
                    Email = userSignUp.Email,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(appUser, userSignUp.Password);
                if (result.Succeeded)
                {
                  
                    return RedirectToAction("SignIn", "Login");
                }
            }

            return View();
        }
    }
}

