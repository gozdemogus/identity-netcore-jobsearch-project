using System;
namespace BaseIdentity.EntityLayer.Concrete
{
	public class Friendship
	{
		public Friendship()
		{
		}

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int FriendId { get; set; }
        public AppUser Friend { get; set; }
    }
}

