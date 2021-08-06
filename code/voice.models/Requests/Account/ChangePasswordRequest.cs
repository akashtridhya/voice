using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class ChangePasswordRequest : PasswordRequest
    {
        [Required(ErrorMessage = "bad_response_confirm_password_required")]
        [Compare("Password", ErrorMessage = "bad_response_passwprd_not_match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "bad_response_old_password_required")]
        public string CurrentPassword { get; set; }

        [JsonIgnore]
        public Guid? UserId { get; set; }

        [JsonIgnore]
        public string UniqueId { get; set; }
    }
}