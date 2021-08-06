using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace voice.middleware.Models.General.Country.Request
{
    public class CountryPersistRequest
    {
        public string id { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string languageIdCsv { get; set; }
        public string[] selectedLanguage { get; set; }
        public string currencyIdCsv { get; set; }
        public string[] selectedCurrency { get; set; }


    }
}