using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class AttachmentUploadRequest : FileUploadRequest
    {
        [JsonPropertyName("type")]
        [Required(ErrorMessage = "bad_response_file_upload_type_required")]
        public string Type { get; set; }
    }
}