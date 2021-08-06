using Newtonsoft.Json;

namespace voice.middleware.Models.Dashboard.Response
{
    public class ProfileImageResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}