namespace voice.middleware.Models.Token.Request
{
    public class PersistsTokenRequest
    {
        public string id { get; set; }
        public string tokenName { get; set; }
        public string tokenDescription { get; set; }
        public int tokenAmount { get; set; }
        public string productId { get; set; }
    }
}