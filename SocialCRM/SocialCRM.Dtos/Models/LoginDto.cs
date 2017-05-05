using System.ComponentModel.DataAnnotations;

namespace SocialCRM.Dtos.Models
{
    public class LoginDto
    {
        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required, Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}