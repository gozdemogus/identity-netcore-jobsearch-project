using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseIdentity.PresentationLayer.ViewComponents
{
    public class Navigation : ViewComponent
    {
        public Navigation()
        {
        }

        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}

