using System;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class UniqueIdRequest
    {
        [JsonIgnore]
        public Guid UniqueId { get; set; }

        public UniqueIdRequest()
        {
            UniqueId = Guid.NewGuid();
        }

        [JsonIgnore]
        public Guid? UserId { get; set; }

        //[JsonIgnore]
        //public string UniqueIdForUserManagement { get; set; }
    }
}