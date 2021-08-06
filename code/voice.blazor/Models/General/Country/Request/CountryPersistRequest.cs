using System;

namespace voice.blazor.Models.General.Country.Request
{
    public class CountryPersistRequest
    {
        public string id { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
    }
}