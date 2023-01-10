using System;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.BusinessLayer.Abstract
{
	public interface IMessageService : IGenericService<Message>
    {
        List<Message> GetMessageByReceiver(int receiverID);
       
        List<Message> GetMessageBySender(int senderID);

        List<Message> GetMessageByReceiverUsername(string receiverUsername);

        List<Message> GetMessageBySenderUsername(string senderUsername);
    }
}

