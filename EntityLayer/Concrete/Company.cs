using System;
namespace BaseIdentity.EntityLayer.Concrete
{
	public class Company
	{
		public Company()
		{
		}

		public int CompanyID { get; set; }
		public string CompanyName { get; set; }
		public string? Sector { get; set; }
		public string? CompanyAddress { get; set; }
		public string? Website { get; set; }
		public string? EmployeeQuantity { get; set; }
		public string? CompanyMail { get; set; }
		public string? From { get; set; }
		public string? Logo { get; set; }
		public string? Linkedin { get; set; }
        public string? Instagram { get; set; }
        public string? Google { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Explanation { get; set; }
        public ICollection<Ad> Ads { get; set; }
    }
}

