using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace voice.middleware.Models.Account
{
    public class ForgetPasswordRequest
    {
        [JsonProperty("emailId")]
        [Required(ErrorMessage = "Please Enter Email Address!")]
        public string Email { get; set; }
    }
}