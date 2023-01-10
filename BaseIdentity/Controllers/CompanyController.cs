using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyManager;

        public CompanyController(ICompanyService companyManager)
        {
            _companyManager = companyManager;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {

            List<Company> companies = _companyManager.TGetList();
            return View(companies);
        }


        public IActionResult Detail(int id)
        {

            Company companies = _companyManager.TGetByID(id);
            return View(companies);
        }
    }
}

