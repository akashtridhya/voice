using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class UserActiveTimeInsertRequest : BaseValidateRequest
    {
        [Required(ErrorMessage = "bad_response_start_time_required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "bad_response_end_time_required")]
        public DateTime EndTime { get; set; }

        public DateTime StartGameTime { get; set; }

        public DateTime EndGameTime { get; set; }

        [JsonIgnore]
        public string Role { get; set; }
    }
}