using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models.Requests.UserManagement
{
    public class UsersSelectActiveTimeRequest : BaseValidateRequest
    {
        [Required(ErrorMessage = "bad_response_user_required")]
        public Guid user { get; set; }

        [JsonIgnore]
        public string Role { get; set; }
    }
}
