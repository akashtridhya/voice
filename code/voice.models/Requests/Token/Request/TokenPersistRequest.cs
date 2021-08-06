namespace voice.models.Requests.Token.Request
{
    public class TokenPersistRequest : BaseIdRequest
    {
        public string TokenName { get; set; }

        public string TokenDescription { get; set; }

        public decimal TokenAmount { get; set; }

        public string ProductId { get; set; }
    }
}