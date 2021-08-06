using System.Text.Json.Serialization;

namespace voice.models.Requests.Game
{
    public class GetGameListRequest : BaseValidateRequest
    {
        public string GameCategory{ get; set; }

        [JsonIgnore]
        public string Role { get; set; }
    }
}