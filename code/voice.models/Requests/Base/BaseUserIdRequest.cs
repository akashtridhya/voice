using System;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class BaseUserIdRequest
    {
        [JsonIgnore]
        public Guid? UserId { get; set; }
    }
}