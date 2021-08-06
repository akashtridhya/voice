using System;
using System.ComponentModel.DataAnnotations;

namespace voice.models
{
    public class CustomerSelectDetailsRequest : BaseValidateRequest
    {
        [Required(ErrorMessage = "bad_response_id_required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "bad_response_application_number_required")]
        public string ApplicationNumber { get; set; }
    }
}