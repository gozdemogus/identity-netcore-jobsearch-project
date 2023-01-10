using System;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.DataAccessLayer.Abstract
{
	public interface IMessageDal : IGenericDal<Message>
    {

        List<Message> GetMessageByReceiver(int receiverID);
        List<Message> GetMessageBySender(int senderID);
        List<Message> GetMessageByReceiverUsername(string receiverUsername);
        List<Message> GetMessageBySenderUsername(string senderUsername);

    }
}

