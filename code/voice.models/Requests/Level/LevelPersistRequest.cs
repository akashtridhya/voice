using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models.Requests.Level
{
    public class LevelPersistRequest : BaseIdRequest
    {
        [Required(ErrorMessage = "bad_response_level_required")]
        public int Level { get; set; }

        [Required(ErrorMessage = "bad_response_xppoints_required")]
        public int XpPoints { get; set; }

        [JsonIgnore]
        public string Role { get; set; }
    }
}
