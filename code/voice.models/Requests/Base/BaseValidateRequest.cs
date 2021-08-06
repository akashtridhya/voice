using System;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class BaseValidateRequest
    {
        [JsonIgnore]
        public Guid? UserId { get; set; }

        [JsonIgnore]
        public string UniqueId { get; set; }
    }
}