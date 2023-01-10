using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.ViewComponents
{
    public class LayoutHeader : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public LayoutHeader(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            if( User.Identity.Name != null)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                ViewBag.Signed = true;
                if(user != null)
                {
                    ViewBag.User = user.Name;
                }
            }
            else { ViewBag.Signed = false;
            }

            return View();
        }
    }
}

