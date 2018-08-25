using System.ComponentModel.DataAnnotations;

namespace TaskList.Models
{
    public class LoginModel
    {
        public LoginModel()
        {

        }

        [Required(ErrorMessage = "Email is required for login")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required for login")]
        public string Password { get; set; }
    }
}