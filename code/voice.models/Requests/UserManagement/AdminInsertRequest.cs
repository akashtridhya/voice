using System.Text.Json.Serialization;

namespace voice.models
{
    public class AdminInsertRequest : UniqueIdRequest
    {
        public string UserName { get; set; }
       
        public string EmailId { get; set; }

        public RoleEnums Role { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}