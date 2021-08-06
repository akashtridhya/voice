using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class FileUploadRequest : BaseValidateRequest
    {
        [JsonPropertyName("file")]
        [Required(ErrorMessage = "bad_response_file_required")]
        public IFormFile File { get; set; }
    }
}