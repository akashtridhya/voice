namespace voice.models.Requests.General
{
    public class CountryPersistRequest : BaseIdRequest
    {
        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string LanguageIdCsv { get; set; }

        public string CurrencyIdCsv { get; set; }
    }
}