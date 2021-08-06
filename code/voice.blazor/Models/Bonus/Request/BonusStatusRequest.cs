namespace voice.blazor.Models.Bonus.Request
{
    public class BonusStatusRequest
    {
        public string id { get; set; }

        public bool active { get; set; }

        public bool deleted { get; set; }
    }
}
