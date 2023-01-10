using System;
using Microsoft.AspNetCore.Mvc;

namespace BaseIdentity.PresentationLayer.ViewComponents
{
	public class Statistics:ViewComponent
	{
		public Statistics()
		{
		}

        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}

