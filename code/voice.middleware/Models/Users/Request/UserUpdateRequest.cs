using System;

namespace voice.middleware.Models.Users.Request
{
    public class UserUpdateRequest
    {
        public string id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string birthDate { get; set; }
        public string email { get; set; }
        public int houseNumber { get; set; }
        public int postcode { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string fbId { get; set; }
    }
}
