namespace voice.models.Requests.Ads.Request
{
    public class AdsPersistRequest : BaseIdRequest
    {
        public string AdName { get; set; }
        public string AdImageURL { get; set; }
    }
}