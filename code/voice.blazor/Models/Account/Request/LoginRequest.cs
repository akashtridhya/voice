using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace voice.blazor.Models.Account
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Please Enter Username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Password.")]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}