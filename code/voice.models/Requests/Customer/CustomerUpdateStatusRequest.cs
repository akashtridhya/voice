using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class CustomerUpdateStatusRequest : BaseRequiredIdRequest
    {
        [Required(ErrorMessage = "bad_response_application_number_required")]
        public string ApplicationNumber { get; set; }

        [Required(ErrorMessage = "bad_response_status_required")]
        public string Status { get; set; }

        [JsonIgnore]
        public string StatusByDma { get; set; }

        [JsonIgnore]
        public string StatusByGa { get; set; }

        [JsonIgnore]
        public Guid PerformedById { get; set; }
        
        public string Remarks { get; set; }

        public string Name { get; set; }
    }
}