using System;
using Microsoft.AspNetCore.Identity;

namespace BaseIdentity.EntityLayer.Concrete
{
	public class AppUser:IdentityUser<int>
	{
		public AppUser()
		{
		}

		public int AppUserId { get; set; }
		public string? Gender { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string? Image { get; set; }
        public string? Explanation { get; set; }
        public string? Skills { get; set; }
        public string? Linkedin { get; set; }
        public string? Twitter { get; set; }
        public DateTime DateOfBirth { get; set; }
		public string? CurrentWork { get; set; }
        public string? ExWork1 { get; set; }
        public string? ExWork2 { get; set; }
        public string? Languages { get; set; }
        public string? Title { get; set; }
		public string? UniversityName { get; set; }
		public string? DepartmentofUniversity { get; set; }
		public string? Courses { get; set; }
		public bool? DriverLicense { get; set; }
        public virtual ICollection<Message> UserSender { get; set; }
        public virtual ICollection<Message> UserReceiver { get; set; }
		public bool? IsActive { get; set; }

        public virtual ICollection<AdAppUser> AdAppUsers { get; set; }


        public virtual ICollection<Friendship> Friendships { get; set; }

    }
}

