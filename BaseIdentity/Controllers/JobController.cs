using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class JobController : Controller
    {

        private readonly IAdService _AdManager;


        public JobController(IAdService AdManager)
        {

            _AdManager = AdManager;
        }


        public IActionResult Index()
        {
            var values = _AdManager.ListJobsWithDetail();
            return View(values);
        }


        [HttpGet]
        public IActionResult Detail(int id)
        {
           var values = _AdManager.SearchJobWithDetail(id);
            return View(values);
        }


        [HttpPost]
        // GET: /<controller>/
        public IActionResult SearchJob(SearchViewModel searchViewModel)
        {
            var ad = new List<Ad>();
            using (var context = new Context())
            {
                //keyword yoksa
                if (searchViewModel.keyword == null)
                {

                    //experience ve category ikisi de varsa
                    if (searchViewModel.experience != null && searchViewModel.category != null)
                    {
                        ad = _AdManager.ListJobsWithDetail()
                       .Where(x => x.Experience.Name == searchViewModel.experience && x.Category.CategoryName == searchViewModel.category)
                       .ToList();

                        if (ad.Count != 0)
                        {
                            ViewBag.type = ad.FirstOrDefault().Category.CategoryName + " and " + ad.FirstOrDefault().Experience.Name;

                        }

                    }

                    //yalnızca experience varsa
                    if (searchViewModel.experience != null && searchViewModel.category == null)
                    {
                        ad = _AdManager.ListJobsWithDetail()
                       .Where(x => x.Experience.Name == searchViewModel.experience)
                       .ToList();

                        if (ad.Count != 0)
                        {
                            ViewBag.type = ad.FirstOrDefault().Experience.Name;
                        }
                    }

                    //yalnızca kategori varsa
                    if (searchViewModel.experience == null && searchViewModel.category != null)
                    {
                        ad = _AdManager.ListJobsWithDetail()
                       .Where(x => x.Category.CategoryName == searchViewModel.category)
                       .ToList();

                        if (ad.Count != 0)
                        {
                            ViewBag.type = ad.FirstOrDefault().Category.CategoryName;
                        }
                    }
                }
                else
                {
                    var lowerKeyword = searchViewModel.keyword.ToLower();

                    //experience ve category ikisi de varsa
                    if (searchViewModel.experience != null && searchViewModel.category != null)
                    {
                        ad =_AdManager.ListJobsWithDetail()
                       .Where(x => x.Experience.Name == searchViewModel.experience && x.Category.CategoryName == searchViewModel.category && x.Company.CompanyName.ToLower().Contains(lowerKeyword))
                       .ToList();

                        if (ad.Count != 0)
                        {
                            ViewBag.type = ad.FirstOrDefault().Category.CategoryName + " and " + ad.FirstOrDefault().Experience.Name;

                        }

                    }

                    //yalnızca experience varsa
                    if (searchViewModel.experience != null && searchViewModel.category == null)
                    {
                       var sonuc = _AdManager.ListJobsWithDetail();

                        ad = _AdManager.ListJobsWithDetail()
                       .Where(x => x.Experience.Name == searchViewModel.experience && x.Company.CompanyName.ToLower().Contains(lowerKeyword))
                       .ToList();

                        if (ad.Count != 0)
                        {
                            ViewBag.type = ad.FirstOrDefault().Experience.Name;
                        }
                    }

                    //yalnızca kategori varsa
                    if (searchViewModel.experience == null && searchViewModel.category != null)
                    {
                        ad = _AdManager.ListJobsWithDetail()
                       .Where(x => x.Category.CategoryName == searchViewModel.category && x.Company.CompanyName.ToLower().Contains(lowerKeyword))
                       .ToList();

                        if (ad.Count != 0)
                        {
                            ViewBag.type = ad.FirstOrDefault().Category.CategoryName;
                        }
                    }

                    //hicbiri yok sadece keyword varsa
                    if (searchViewModel.experience == null && searchViewModel.category == null)
                    {
                        ad = _AdManager.ListJobsWithDetail()
                       .Where(x => x.Company.CompanyName.ToLower().Contains(lowerKeyword))
                       .ToList();

                        if (ad.Count != 0)
                        {
                            ViewBag.type = searchViewModel.keyword;
                        }
                    }

                }

            }

            var values = ad;
            return View(values);
        }

    }
}

