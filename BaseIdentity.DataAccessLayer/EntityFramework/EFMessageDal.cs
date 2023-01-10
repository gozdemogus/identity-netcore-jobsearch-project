using System;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.DataAccessLayer.Repository;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BaseIdentity.DataAccessLayer.EntityFramework
{
    public class EFMessageDal : GenericRepository<Message>, IMessageDal
    {
        public List<Message> GetMessageByReceiver(int receiverID)
        {
            using (var context = new Context())
            {
                return context.Messages.Include(x => x.ReceiverUser).Include(x=>x.SenderUser).Where(x => x.ReceiverID == receiverID).ToList();
            }
        }

        public List<Message> GetMessageBySender(int senderID)
        {
            using (var context = new Context())
            {
                return context.Messages.Include(x => x.ReceiverUser).Include(x => x.SenderUser).Where(x => x.SenderID == senderID).ToList();
            }
        }


        public List<Message> GetMessageByReceiverUsername(string receiverUsername)
        {
            using (var context = new Context())
            {
                return context.Messages.Include(x => x.ReceiverUser).Include(x => x.SenderUser).Where(x => x.ReceiverUser.UserName == receiverUsername).ToList();
            }
        }

        public List<Message> GetMessageBySenderUsername(string senderUsername)
        {
            using (var context = new Context())
            {
                return context.Messages.Include(x => x.ReceiverUser).Include(x => x.SenderUser).Where(x => x.SenderUser.UserName == senderUsername).ToList();
            }
        }

    }
}

