using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class UserXpTimePersistRequest : BaseValidateRequest
    {
        [JsonIgnore]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "bad_response_time_required")]
        public string XpTime { get; set; }

    }
}