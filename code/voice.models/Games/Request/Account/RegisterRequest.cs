namespace voice.models.Games.Request.Account
{
    public class RegisterRequest
    {
        public string accountSystemTag { get; set; }
        public string countryCallingCode { get; set; }
        public string countryCode { get; set; }
        public string currencyCode { get; set; }
        public string dateOfBirth { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string mobilePhoneNumber { get; set; }
        public string platformTag { get; set; }
        public int privacyPolicyVersion { get; set; }
        public int termsAndCondsVersion { get; set; }
        public bool contactableByUs { get; set; }
        public bool contactableBySms { get; set; }
        public bool contactableByOthers { get; set; }
        public bool contactableByPost { get; set; }
        public bool contactableByCall { get; set; }
    }
}