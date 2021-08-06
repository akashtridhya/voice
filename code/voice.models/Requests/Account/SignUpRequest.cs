using System;
using System.ComponentModel.DataAnnotations;

namespace voice.models
{
    public class SignUpRequest : UniqueIdRequest
    {
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "bad_response_invalid_email")]
        public string EmailId { get; set; }

        [DataType(DataType.Password)]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int HouseNumber { get; set; }

        public int Postcode { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string CountryCode { get; set; }

        public string LanguageCode { get; set; }

        public string CurrencyCode { get; set; }

        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string MobileNumber { get; set; }

        public RoleEnums Role { get; set; }

        public string FbId { get; set; }
    }
}
