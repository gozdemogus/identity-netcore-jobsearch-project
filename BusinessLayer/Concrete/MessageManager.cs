using System;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.BusinessLayer.Concrete
{
	public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void TDelete(Message t)
        {
            _messageDal.Delete(t);
        }

        public Message TGetByID(int id)
        {
            return _messageDal.GetByID(id);
        }

        public List<Message> TGetList()
        {
            return _messageDal.GetList();
        }

        public void TInsert(Message t)
        {
            _messageDal.Insert(t);
        }

        public void TUpdate(Message t)
        {
            _messageDal.Update(t);
        }

        public List<Message> GetMessageByReceiver(int receiverID)
        {
            return _messageDal.GetMessageByReceiver(receiverID);
        }

        public List<Message> GetMessageBySender(int senderID)
        {
            return _messageDal.GetMessageByReceiver(senderID);
        }

        public List<Message> GetMessageByReceiverUsername(string receiverUsername)
        {
            return _messageDal.GetMessageByReceiverUsername(receiverUsername);
        }

        public List<Message> GetMessageBySenderUsername(string senderUsername)
        {
            return _messageDal.GetMessageBySenderUsername(senderUsername);
        }
    }
}

