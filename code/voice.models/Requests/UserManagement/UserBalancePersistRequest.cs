using System;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class UserBalancePersistRequest
    {
        [JsonIgnore]
        public Guid? Id { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }

        public decimal Balance { get; set; }

    }
}