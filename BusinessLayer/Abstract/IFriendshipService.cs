using System;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.BusinessLayer.Abstract
{
	public interface IFriendshipService : IGenericService<Friendship>
    {
        List<Friendship> ListFriends(int id);
    }
}

