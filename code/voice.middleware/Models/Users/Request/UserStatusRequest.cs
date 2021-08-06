using Newtonsoft.Json;

namespace voice.middleware.Models.Users.Request
{
    public class UserStatusRequest
    {
        public string id { get; set; }

        public bool active { get; set; }

        public bool deleted { get; set; }
    }
}