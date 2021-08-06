using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class UserDetailsUpdateRequest : BaseRequiredIdRequest
    {
        [Required(ErrorMessage = "bad_response_first_name_required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "bad_response_last_name_required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "bad_response_mobile_required")]
        public string Mobile { get; set; }

    }
}