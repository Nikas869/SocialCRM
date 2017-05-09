using System.ComponentModel.DataAnnotations;

namespace SocialCRM.Dtos.Models
{
    public class UserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Avatar { get; set; }

        public string Email { get; set; }
    }

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

    public class RegisterDto
    {
        [Required, DataType(DataType.EmailAddress), MaxLength(256), Display(Name = "Email address")]
        public string Email { get; set; }

        [Required, MaxLength(128), Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required, MaxLength(128), Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required, MaxLength(100), MinLength(6)]
        public string Password { get; set; }

        [Required, Compare("Password"), Display(Name = "Password (again)")]
        public string ConfirmPassword { get; set; }
    }
}
