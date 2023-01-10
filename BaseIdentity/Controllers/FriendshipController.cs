using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class FriendshipController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IFriendshipService _friendshipManager;


        public FriendshipController(UserManager<AppUser> userManager, IFriendshipService friendshipManager)
        {
            _userManager = userManager;
            _friendshipManager = friendshipManager;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ListFriends()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var friends = _friendshipManager.ListFriends(user.Id);

            return View(friends);
        }


       
         
        



        [HttpGet]
        public async Task<IActionResult> AddToFriends(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            Friendship friendship = new Friendship();
            friendship.AppUserId = user.Id;
            friendship.FriendId = id;

            _friendshipManager.TInsert(friendship);

            return RedirectToAction("ListFriends","Friendship");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromFriends(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            Friendship friendship = new Friendship();
            friendship.AppUserId = user.Id;
            friendship.FriendId = id;

            _friendshipManager.TDelete(friendship);

            return RedirectToAction("ListFriends", "Friendship");
        }

    }
}

