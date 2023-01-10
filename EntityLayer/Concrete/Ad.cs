using System;
using System.ComponentModel.DataAnnotations;

namespace BaseIdentity.EntityLayer.Concrete
{
	public class Ad
	{
		public Ad()
		{
		}
		[Key]
		public int AdID { get; set; }
		public string AdName { get; set; }
		public string? AdLocation { get; set; }
		public string? Salary { get; set; }
		public string? Description { get; set; }
		public string? AdditionalRights { get; set; }
		public string? Qualifications { get; set; }

		public Category Category { get; set; }

        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        public virtual ICollection<AdAppUser> AdAppUsers { get; set; }

        public int? ExperienceId { get; set; }
        public virtual Experience Experience { get; set; }

    }
}

