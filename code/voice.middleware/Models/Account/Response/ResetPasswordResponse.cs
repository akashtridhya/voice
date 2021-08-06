using Newtonsoft.Json;

namespace voice.middleware.Models.Account.Response
{
    public class ResetPasswordResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}