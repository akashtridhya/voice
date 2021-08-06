using System;
using System.Text.Json.Serialization;
using voice.models.Responses.UserManagement;

namespace voice.models
{
    public class ProfileResponse : BaseAuditResponse
    {
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public decimal Balance { get; set; }

        public int XpPoints { get; set; }

        public string XpTime { get; set; }

        public int Level { get; set; }

        public int UserPosition { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string ProfileURL { get; set; }

        public int HouseNumber { get; set; }

        public int Postcode { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string CountryCode { get; set; }

        public string LanguageCode { get; set; }

        public string CurrencyCode { get; set; }
        
        public string MobileNumber { get; set; }

        [JsonIgnore]
        public string FbId { get; set; }

        public bool IsFbRegistered { get; set; }

        public string SessionCorrelationId { get; set; }
        public string UserCorrelationId { get; set; }

        public UserLeaderBoardResponse SingleOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }

    public class ProfileWithTokenResponse : ProfileResponse
    {
        public string Token { get; set; }
    }
}