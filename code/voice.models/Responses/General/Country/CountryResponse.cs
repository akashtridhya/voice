using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace voice.models.Responses.General.Country
{
    public class CountryResponse
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }

        [JsonIgnore]
        public string LanguageJson { get; set; }
        public List<LanguageDetail> LanguageNameCsv { get; set; }

        [JsonIgnore]
        public string CurrencyJson { get; set; }
        public List<CurrencyDetail> CurrencyNameCsv { get; set; }
    }

    public class LanguageDetail
    {
        public string Id { get; set; }

        public string Language { get; set; }

        public string LanguageCode { get; set; }
    }

    public class CurrencyDetail
    {
        public string Id { get; set; }

        public string Currency { get; set; }

        public string CurrencyCode { get; set; }
    }
}
