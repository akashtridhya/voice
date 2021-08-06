using System.Text.Json.Serialization;

namespace voice.models
{
    public class PaginationResponse
    {
        [JsonIgnore]
        public int Total { get; set; }

        [JsonIgnore]
        public int OffSet { get; set; }
    }
}