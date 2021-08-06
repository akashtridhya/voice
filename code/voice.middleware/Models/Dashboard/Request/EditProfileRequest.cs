using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace voice.middleware.Models.Dashboard.Request
{
    public class ProfileUpdateRequest
    {
        public IFormFile File { get; set; }
    }

    public class EditProfileRequest: ProfileUpdateRequest
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ProfileUrl { get; set; }

    }
}