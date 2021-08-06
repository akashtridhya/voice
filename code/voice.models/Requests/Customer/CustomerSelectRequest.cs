using System;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class CustomerSelectRequest : SearchParamRequest
    {
        public string Status { get; set; }

        [JsonIgnore]
        public Guid? DmaStaffId { get; set; }
    }
}