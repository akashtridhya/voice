using System.ComponentModel.DataAnnotations;

namespace voice.models
{
    public class ResetPasswordRequest : ResetPasswordTokenRequest
    {
        [Required(ErrorMessage = "bad_response_password_required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$")]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "bad_response_passwprd_not_match")]
        public string ConfirmPassword { get; set; }
    }
}