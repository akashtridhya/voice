using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace voice.blazor.Models.Generic.Request
{
    public class ImageUploadRequest
    {
        [JsonProperty("file")]
        public IFormFile File { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}