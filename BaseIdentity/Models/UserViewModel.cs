using System;
namespace BaseIdentity.PresentationLayer.Models
{
	public class UserViewModel
	{
		public UserViewModel()
		{
		}

      
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Image { get; set; }
    }
}

