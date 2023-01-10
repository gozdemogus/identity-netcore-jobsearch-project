using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class ApplicationController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public ApplicationController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var ads = new List<Ad>();
            using (var context = new Context())
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var userId = user.Id;
                ads = context.Ads.Where(a => a.AdAppUsers.Any(u => u.AppUserId == userId)).Include(a => a.Company).Include(a => a.Category).ToList();
            }
            return View(ads);
        }


        public async Task<IActionResult> Apply(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            using (var context = new Context())
            {
                var userId = user.Id;
                var adId = id;
                var adAppUser = new AdAppUser
                {
                    AppUserId = userId,
                    AdId = adId
                };

                //many to many iliskili tablo ile kisinin basvurduğu ilan ve kisiye dair her sey geliyor.
                //var x = context.AppUserAds.Where(x => adAppUser.AdId == 1).Include(x => x.AppUser).Include(y => y.Ad).FirstOrDefault();

                context.AppUserAds.Add(adAppUser);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Rollback(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            using (var context = new Context())
            {
                var userId = user.Id;
                var adId = id;
                var adAppUser = new AdAppUser
                {
                    AppUserId = userId,
                    AdId = adId
                };

                
                context.AppUserAds.Remove(adAppUser);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }



    }
}

