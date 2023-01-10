using System;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.BusinessLayer.Concrete
{
	public class FriendShipManager:IFriendshipService
	{
        private readonly IFriendshipDal _friendshipDal;

        public FriendShipManager(IFriendshipDal friendshipDal)
        {
            _friendshipDal = friendshipDal;
        }

        
        public List<Friendship> ListFriends(int id)
        {
            return _friendshipDal.ListFriends(id);
        }

        public void TDelete(Friendship t)
        {
             _friendshipDal.Delete(t);
        }

        public Friendship TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Friendship> TGetList()
        {
            return _friendshipDal.GetList();
        }

        public void TInsert(Friendship t)
        {
             _friendshipDal.Insert(t);
        }

        public void TUpdate(Friendship t)
        {
            throw new NotImplementedException();
        }
    }
}

