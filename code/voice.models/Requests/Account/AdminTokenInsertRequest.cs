namespace voice.models.Requests.Account
{
    public class AdminTokenInsertRequest : BaseIdRequest
    {
        public bool Flag { get; set; }

        public string Token { get; set; }

    }
}
