using System;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseIdentity.PresentationLayer.ViewComponents.Chat
{
	public class ReplyMessage:ViewComponent
	{
        public ReplyMessage()
        {
           
        }

        public IViewComponentResult Invoke(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}

