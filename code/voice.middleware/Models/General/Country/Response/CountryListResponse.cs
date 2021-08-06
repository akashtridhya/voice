using System;
using System.Collections.Generic;

namespace voice.middleware.Models.General.Country.Response
{
    public class cityDetail
    {
        public string Id { get; set; }
        public int number { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string LanguageName { get; set; }

        public string LanguageIdCsv { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyIdCsv { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class CountryListResponse
    {
        public List<cityDetail> details { get; set; }
    }

}