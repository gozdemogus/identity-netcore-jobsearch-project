using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class MessageController : Controller
    {

        private readonly IMessageService _messageManager;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(UserManager<AppUser> userManager, IMessageService messageManager)
        {
            _userManager = userManager;
            _messageManager = messageManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(int id)
        {
            if ( id != 0)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var receiverMessages = _messageManager.GetMessageByReceiver(user.Id).Where(x=>x.SenderID==id);
                var senderMessages = _messageManager.GetMessageBySender(id).Where(x=>x.SenderID== user.Id);

                //iki kisi arasındaki tüm mesajları toplayıp bir araya getiriyorum ki chat icinde görelim
                List<Message> conversationHistory = new List<Message>();
                conversationHistory.AddRange(receiverMessages);
                conversationHistory.AddRange(senderMessages);


                //chat detayları icin 
                ViewBag.Sender = receiverMessages.FirstOrDefault().SenderUser.Name;
                ViewBag.SenderImg = receiverMessages.FirstOrDefault().SenderUser.Image;
                ViewBag.SenderActivity = receiverMessages.FirstOrDefault().SenderUser.IsActive;
                ViewBag.SendMessages = conversationHistory.OrderBy(x => x.MessageDate);
                ViewBag.SenderID = user.Id;
                ViewBag.ReceiverID = receiverMessages.FirstOrDefault().SenderID;

                return View();
            }

            else
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageAsync(Message MessageContent)
        {
            var username = MessageContent.ReceiverUser.UserName;
            var receiver = _userManager.Users.Where(x => x.UserName == username).First();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            Message message = new Message();
            message.ReceiverID = receiver.Id;
            message.MessageDate = DateTime.Now;
            message.SenderID = user.Id;
            message.MessageStatus = true;
            message.MessageDetails = MessageContent.MessageDetails;
            message.Subject = MessageContent.Subject;
            message.SenderUsername = user.UserName;
            message.ReceiverUsername = receiver.UserName;

            _messageManager.TInsert(message);

            return RedirectToAction("Index", "Message");
        }


        [HttpPost]
        public async Task<IActionResult> ReplyMessageAsync(Message MessageContent)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userReceiver = await _userManager.FindByIdAsync(MessageContent.ReceiverID.ToString());

            Message message = new Message();
            message.ReceiverID = MessageContent.ReceiverID;
            message.MessageDate = DateTime.Now;
            message.SenderID = user.Id;
            message.MessageStatus = true;
            message.MessageDetails = MessageContent.MessageDetails;
            message.Subject = "reply message";
            message.SenderUsername = user.UserName;
            message.ReceiverUsername = userReceiver.UserName;

            _messageManager.TInsert(message);

            return Redirect($@"/Message/Index/{message.ReceiverID}");
        }

    }
}

