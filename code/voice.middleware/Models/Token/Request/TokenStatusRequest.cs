namespace voice.middleware.Models.Token.Request
{
    public class TokenStatusRequest
    {
        public string id { get; set; }

        public bool active { get; set; }

        public bool deleted { get; set; }
    }
}
