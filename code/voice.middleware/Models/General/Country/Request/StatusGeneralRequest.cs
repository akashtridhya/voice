namespace voice.middleware.Models.General.Country.Request
{
    public class StatusGeneralRequest
    {
        public string id { get; set; }

        public bool active { get; set; }

        public bool deleted { get; set; }
    }
}
