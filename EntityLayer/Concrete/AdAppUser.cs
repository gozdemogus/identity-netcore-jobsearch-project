using System;
namespace BaseIdentity.EntityLayer.Concrete
{
	public class AdAppUser
	{
		public AdAppUser()
		{
		}
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int AdId { get; set; }
        public Ad Ad { get; set; }
    }
}

