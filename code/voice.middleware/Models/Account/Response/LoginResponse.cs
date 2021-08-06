using System;

namespace voice.middleware.Models.Account.Response
{
    public class LoginResponse : CallAPIList
    {
        public string token { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string profileURL { get; set; }
        public bool isFbRegistered { get; set; }
        public bool active { get; set; }
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string id { get; set; }
    }

}