using System.ComponentModel.DataAnnotations;

namespace AuthExample.Classes
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
