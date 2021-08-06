namespace voice.models.Requests.General
{
    public class CurrencyPersistRequest : BaseIdRequest
    {
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }
    }
}