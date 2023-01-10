using System;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.DataAccessLayer.Repository;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BaseIdentity.DataAccessLayer.EntityFramework
{
    public class EFFriendshipDal : GenericRepository<Friendship>, IFriendshipDal
    {
        public EFFriendshipDal()
        {
        }

        public List<Friendship> ListFriends(int id)
        {
            using (var context = new Context())
            {

                List<Friendship> friends = context.Friendships
                                            .Include(f => f.Friend)
                                             .Where(x => x.AppUserId == id)
                                                .ToList();
                return friends;
            }
        }
    }
}

