using Newtonsoft.Json;

namespace voice.middleware.Models.Account.Response
{
    public class PasswordFlagResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}