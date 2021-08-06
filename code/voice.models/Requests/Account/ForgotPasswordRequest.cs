using System.ComponentModel.DataAnnotations;

namespace voice.models
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string EmailId { get; set; }
    }
}