using System;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BaseIdentity.BusinessLayer.Abstract;
using System.Linq;

namespace BaseIdentity.PresentationLayer.ViewComponents
{
	public class Banner:ViewComponent
	{
        private readonly IExperienceService _experienceService;
        private readonly ICategoryService _categoryService;

        public Banner(IExperienceService experienceService, ICategoryService categoryService)
        {
            _experienceService = experienceService;
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {

            List<SelectListItem> experiencevalues = (from x in _experienceService.TGetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.Name.ToString()
                                                   }).ToList();
            experiencevalues.Insert(0, new SelectListItem { Text = "Experience", Value = string.Empty });

            ViewBag.exp = experiencevalues;



            List<SelectListItem> categoryvalues = (from x in _categoryService.TGetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryName.ToString()
                                                   }).ToList();
            categoryvalues.Insert(0, new SelectListItem { Text = "Category", Value = string.Empty });
            ViewBag.cat = categoryvalues;
            return View();
        }
    }
}

