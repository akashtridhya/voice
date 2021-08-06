using System;
using System.ComponentModel.DataAnnotations;

namespace voice.models
{
    public class BaseRequiredIdRequest : BaseValidateRequest
    {
        [Required(ErrorMessage = "bad_response_id_required")]
        public Guid Id { get; set; }
    }
}