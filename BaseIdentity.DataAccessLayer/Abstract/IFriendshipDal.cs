using System;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.DataAccessLayer.Abstract
{
	public interface IFriendshipDal : IGenericDal<Friendship>
    {
        List<Friendship> ListFriends(int id);

    }
}

