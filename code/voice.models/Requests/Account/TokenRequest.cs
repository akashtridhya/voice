using System.ComponentModel.DataAnnotations;

namespace voice.models
{
    public class ResetPasswordTokenRequest
    {
        [Required]
        public string Token
        {
            get => _Token;
            set => _Token = value.Replace(" ", "+");
        }

        private string _Token { get; set; }
    }
}