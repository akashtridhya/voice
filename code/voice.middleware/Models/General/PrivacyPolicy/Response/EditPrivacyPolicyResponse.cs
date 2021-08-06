using Newtonsoft.Json;

namespace voice.middleware.Models.General.PrivacyPolicy.Response
{
    public class EditPrivacyPolicyResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}