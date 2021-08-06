namespace voice.models
{
    public class SetShortURLRequest
    {
        public string SourceURL { get; set; }
    }

    public class GetSourceURLRequest
    {
        public string ShortURL { get; set; }
    }
}