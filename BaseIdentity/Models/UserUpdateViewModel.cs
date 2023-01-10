using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BaseIdentity.PresentationLayer.Models
{
	public class UserUpdateViewModel
	{
		public UserUpdateViewModel()
		{
		}


        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
      
        public string Explanation { get; set; }
        public string Skills { get; set; }
        public string Linkedin { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CurrentWork { get; set; }
        public string Title { get; set; }

        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Type existing password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Type new password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Passwords must match")]
        public string ConfirmNewPassword { get; set; }
        public string Token { get; set; }
    }
}

