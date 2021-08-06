using System;
using System.Collections.Generic;

namespace voice.blazor.Models.Users.Response
{
    public class Detail
    {
        public int number { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Birthdate { get; set; }
        public string HouseNumber { get; set; }
        public string Postcode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string MobileNumber { get; set; }
        public string CountryCode { get; set; }
        public string LanguageCode { get; set; }
        public string CurrencyCode { get; set; }
        public string ProfileUrl { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class UsersListResponse
    {
        public IEnumerable<Detail> details { get; set; }
    }
}