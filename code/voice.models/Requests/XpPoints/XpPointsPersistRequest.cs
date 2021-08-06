using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models.Requests.XpPoints
{
    public class XpPointsPersistRequest : BaseIdRequest
    {
        [Required(ErrorMessage = "bad_response_time_required")]
        public int XpTime { get; set; }

        [Required(ErrorMessage = "bad_response_value_required")]
        public int XpValue { get; set; }

        [JsonIgnore]
        public string Role { get; set; }
    }
}
