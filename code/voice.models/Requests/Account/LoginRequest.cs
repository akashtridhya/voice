using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class LoginRequest : PasswordRequest
    {
        [JsonIgnore]
        [EmailAddress(ErrorMessage = "bad_response_invalid_email")]
        public string EmailId { get; set; }

        public string Username { get; set; }

        public string FbId { get; set; }

        [JsonIgnore]
        public Guid? UserId { get; set; }

        [JsonIgnore]
        public string Role { get; set; }

        [JsonIgnore]
        public string UniqueId { get; set; }
    }

    public class PasswordRequest
    {
        //[Required(ErrorMessage = "bad_response_password_required")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Password must contain the Alphabet, Digits and Special charaters with atleast 8 characters.")]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Password { get; set; }
    }
}