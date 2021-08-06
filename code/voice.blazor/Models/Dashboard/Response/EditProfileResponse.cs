using Newtonsoft.Json;

namespace voice.blazor.Models.Dashboard.Response
{
    public class EditProfileResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}