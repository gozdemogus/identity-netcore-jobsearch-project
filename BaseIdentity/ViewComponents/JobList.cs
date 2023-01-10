using System;
using System.Collections.Generic;
using System.Linq;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseIdentity.PresentationLayer.ViewComponents
{
    public class JobList: ViewComponent
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IAdService _AdManager;


        public JobList(UserManager<AppUser> userManager, IAdService AdManager)
        {
            _userManager = userManager;
            _AdManager = AdManager;
        }

        public IViewComponentResult Invoke()
        {
            var ad= new List<Ad>();

            ad = _AdManager.ListJobsWithDetail().Take(3).ToList();

            return View(ad);
        }
    }
}

