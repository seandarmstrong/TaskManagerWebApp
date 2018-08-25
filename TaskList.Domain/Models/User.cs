using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required for registration")]
        [StringLength(50, ErrorMessage = "First name must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required for registration")]
        [StringLength(50, ErrorMessage = "Last name must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Department is required for registration")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Username is required for registration")]
        [StringLength(15, ErrorMessage = "Username must be between {2} and {1} characters long.", MinimumLength = 4)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email address is required for registration")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required for registration")]
        [StringLength(15, ErrorMessage = "Password must be between {2} and {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your password before continuing")]
        [Compare("Password", ErrorMessage = "The password you're entering must match the password you entered above")]
        public string ConfirmPassword { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
