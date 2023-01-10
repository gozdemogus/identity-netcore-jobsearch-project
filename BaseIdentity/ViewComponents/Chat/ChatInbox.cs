using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.BusinessLayer.Concrete;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace BaseIdentity.PresentationLayer.ViewComponents.Chat
{
    public class ChatInbox : ViewComponent
    {
        private readonly IMessageService _messageManager;
        private readonly UserManager<AppUser> _userManager;

        public ChatInbox(UserManager<AppUser> userManager, IMessageService messageManager)
        {
            _userManager = userManager;
            _messageManager = messageManager;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _messageManager.GetMessageByReceiver(user.Id);
            var valuesDistinct = values.GroupBy(x => x.SenderID).Select(x => x.First()).ToList();
            var receiverMessages = _messageManager.GetMessageByReceiver(user.Id);

            if (values.Count != 0 && valuesDistinct.Count != 0)
            {
                List<Message> conversationHistory = new List<Message>();

                foreach (var item in _userManager.Users)
                {
                    //giris yapan kullanıcıya gelen tüm mesajlar
                    var activeUserInbox = _messageManager.GetMessageByReceiver(user.Id).ToList();
                    if (item.Id == user.Id)
                    {
                        //giris yapan kullanıcının gönderdiği tüm mesajlar
                        var activeUserSendbox = _messageManager.GetMessageBySender(item.Id).ToList();

                        conversationHistory.AddRange(activeUserSendbox);
                    }
                    conversationHistory.AddRange(activeUserInbox);
                }

                var lastValuesDistinct = conversationHistory.OrderByDescending(x => x.MessageDate).GroupBy(x => x.SenderID).Select(x => x.First()).Where(x => x.SenderID != user.Id).ToList();

                var m1 = _messageManager.GetMessageBySenderUsername(user.UserName);
                var m2 = _messageManager.GetMessageByReceiverUsername(user.UserName);

                List<Message> messagesbyUser = new List<Message>();
                messagesbyUser.AddRange(m1);
                messagesbyUser.AddRange(m2);

                //List<List<Message>> ml = new List<List<Message>>();

                var groupedMessages = messagesbyUser.OrderByDescending(x => x.MessageDate).GroupBy(m => m.ReceiverUser.UserName == user.UserName ? m.SenderUser.UserName : m.ReceiverUser.UserName);
                var selectedMessages = groupedMessages.Select(group => group.First()).ToList();

                ViewBag.currentUserId = user.Id;
                return View(selectedMessages);
            }

            else
            {
                ViewBag.currentUserId = user.Id;
                return View();
            }
        }

    }
}

