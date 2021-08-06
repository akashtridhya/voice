using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace voice.middleware.Models.Account
{
    public class ResetPasswordRequest
    {

        [JsonProperty("password")]
        [Required(ErrorMessage = "Please Enter Your New Password!")]
        public string Password { get; set; }

        [JsonProperty("confirmPassword")]
        [Required(ErrorMessage = "Please Enter Confirm Password!")]
        public string ConfirmPassword { get; set; }

        [JsonProperty("token")]
        [Required(ErrorMessage = "Please try after sometime!")]
        public string Token { get; set; }

    }
}