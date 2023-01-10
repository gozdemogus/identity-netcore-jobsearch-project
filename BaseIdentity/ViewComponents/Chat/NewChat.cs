using System;
using BaseIdentity.BusinessLayer.Concrete;
using BaseIdentity.DataAccessLayer.Concrete;
using System.Threading.Tasks;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseIdentity.PresentationLayer.ViewComponents.Chat
{
	public class NewChat:ViewComponent
	{
		public NewChat()
		{
		}

        public IViewComponentResult Invoke()
        {
          
            return View();
        }


    }
}

