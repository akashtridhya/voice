namespace voice.models.Requests.General
{
    public class LanguagePersistRequest : BaseIdRequest
    {
        public string Language { get; set; }

        public string LanguageCode { get; set; }

    }
}