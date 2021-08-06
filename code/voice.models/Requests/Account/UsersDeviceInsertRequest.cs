using System.ComponentModel.DataAnnotations;

namespace voice.models
{
    public class UsersDeviceInsertRequest : BaseValidateRequest
    {
        [Required(ErrorMessage = "bad_response_device_type_required")]
        public string DeviceType { get; set; }

        [Required(ErrorMessage = "bad_response_device_id_required")]
        public string DeviceId { get; set; }
    }
}