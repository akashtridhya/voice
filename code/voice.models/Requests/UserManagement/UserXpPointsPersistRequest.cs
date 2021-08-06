using System;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class UserXpPointsPersistRequest
    {
        [JsonIgnore]
        public Guid? Id { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }

        public int XpPoints { get; set; }

    }
}