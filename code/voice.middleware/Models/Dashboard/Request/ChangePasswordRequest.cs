using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace voice.middleware.Models.Dashboard.Request
{
    public class ChangePasswordRequest
    {
        [JsonProperty("currentPassword")]
        [Required(ErrorMessage = "Please Enter Old Password!")]
        public string OldPassword { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "Please Enter New Password!")]
        public string NewPassword { get; set; }

        [JsonProperty("confirmPassword")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
        [Required(ErrorMessage = "Please Enter Confirm Password!")]
        public string ConfirmPassword { get; set; }
    }
}