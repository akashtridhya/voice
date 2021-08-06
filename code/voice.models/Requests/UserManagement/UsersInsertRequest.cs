using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class UsersInsertRequest : UniqueIdRequest
    {
        [Required(ErrorMessage = "bad_response_first_name_required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "bad_response_last_name_required")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="bad_response_mobile_required")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "bad_response_email_required")]
        [EmailAddress(ErrorMessage ="bad_response_invalid_email")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "bad_response_role_required")]
        public RoleEnums Role { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}