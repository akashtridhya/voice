using Newtonsoft.Json;

namespace voice.middleware.Models.Generic.Response
{
    public class CheckPermissionResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; } = "";

        public int StatusCode { get; set; }
    }
}